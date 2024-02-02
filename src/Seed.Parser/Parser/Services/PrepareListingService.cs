using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Parser.Extensions;

namespace Parser.Services;

public static class PrepareListingService
{
    public static async ValueTask PrepareAsync()
    {
        var workingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");

        // Retrieve listing categories
        var listingCategoriesFileName = Path.Combine(workingDirectory, "Data", "SeedData", "ListingCategories.json");
        var listingCategories = JsonConvert.DeserializeObject<List<dynamic>>(await File.ReadAllTextAsync(listingCategoriesFileName))!;

        // Validate listing categories
        if (listingCategories.DistinctBy(listingCategory => listingCategory.name).Count() < listingCategories.Count)
            throw new Exception("Duplicate listing category name");

        // Get all listing folders
        var directories = Directory.GetDirectories(Path.Combine(workingDirectory, "Data", "SeedData", "Prepare"))
            .Select(directory => new DirectoryInfo(directory))
            .ToList();

        var listingFiles = directories.Select(
                directory =>
                {
                    var category = listingCategories.FirstOrDefault(
                        category => category.name.ToString().Equals(directory.Name, StringComparison.InvariantCultureIgnoreCase)
                    );

                    if (category is null)
                        throw new Exception("Category not found - " + directory.Name);

                    return (CategoryId: Guid.Parse(category.id.ToString()), Files: Directory.GetFiles(directory.FullName, "*.json").ToList());
                }
            )
            // .Where(directory => enabledCategories.Contains(directory.CategoryId))
            .ToList();

        // Read all listing files and deserialize to listing
        var listings = listingFiles.Select(
                listingFile => listingFile.Files.Select(
                        file =>
                        {
                            // Load json file
                            var jsonObject = JObject.Load(new JsonTextReader(new StreamReader(file)));
                            var searchResultsArray = (JArray)jsonObject["data"]!["presentation"]!["staysSearch"]!["results"]!["searchResults"]!;

                            // Deserialize the 'searchResults' array into a list of Listing models
                            return searchResultsArray.Select(
                                result =>
                                {
                                    return (dynamic)new
                                    {
                                        name = result["listing"]!["name"]!.Value<string>()!,
                                        categoryId = listingFile.CategoryId,
                                        imagesStorageFile = ((JArray)result["listing"]!["contextualPictures"]!)
                                            .Select(image => image["picture"]!.Value<string>()!)
                                            .Take(5)
                                            .ToList(),
                                        location = new
                                        {
                                            city = result["listing"]!["city"]!.Value<string>()!,
                                            latitude = result["listing"]!["coordinate"]!["latitude"]!.Value<decimal>()!,
                                            longitude = result["listing"]!["coordinate"]!["longitude"]!.Value<decimal>()!,
                                        },
                                        price = new
                                        {
                                            currency = Currency.Usd,
                                            amount = result["pricingQuote"]!["rate"]!["amount"]!.Value<decimal>()
                                        }
                                    };
                                }
                            );
                        }
                    )
                    .SelectMany(listings => listings.ToList())
                    .ToList()
            )
            .SelectMany(
                listings =>
                {
                    if (listings.Count < 100)
                        throw new Exception("Less than 100 listings for category - " + listings.First().categoryId);

                    if (listings.Any(listing => listing.imagesStorageFile.Count < 5))
                        throw new Exception(
                            "Less than 5 images for listing - " + listings.First(listing => listing.imagesStorageFile.Count < 5).name
                        );

                    return listings.Take(100).ToList();
                }
            )
            .ToList();

        var listingsResult = await ExecuteAsync(listings);

        // Save listing to file

        var listingsFileName = Path.Combine(workingDirectory, "Data", "SeedData", "Listings.json");

        await File.WriteAllTextAsync(listingsFileName, JsonConvert.SerializeObject(listingsResult, Formatting.Indented));

        // var fileStream = File.Open(listingsFileName, FileMode.Append, FileAccess.Write);
        // var streamWriter = new StreamWriter(fileStream);
        // await streamWriter.WriteAsync(JsonConvert.SerializeObject(listingsResult, Formatting.Indented));
    }

    private static async ValueTask<List<dynamic>> ExecuteAsync(ICollection<dynamic> listings)
    {
        // Validate listings are unique
        // if (listings.DistinctBy(listing => listing.name).Count() < listings.Count)
        // throw new Exception("Duplicate listing name");

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://a0.muscache.com/im/pictures/")
        };

        httpClient.Timeout = TimeSpan.FromMinutes(20);

        var workingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");

        var finishedListings = 0;

        var result = new List<dynamic>();

        var filePath = Path.Combine(workingDirectory, "Data", "SeedData", "Images", "Listings");

        var currentProgress = 0D;

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        foreach (var listingsBatch in listings.Chunk(200))
        {
            result.AddRange(
                await Task.WhenAll(
                    listingsBatch.Select(
                            async listing =>
                            {
                                var images = await Task.WhenAll(
                                    ((List<string>)listing.imagesStorageFile).Select(
                                        async imageUrl =>
                                        {
                                            var storageFileId = Guid.NewGuid();
                                            var imageFileName = $"{storageFileId}.jpg";
                                            var imageFilePath = Path.Combine(filePath, imageFileName);

                                            // Download and save image
                                            await using var imageStream = await httpClient.GetStreamAsync(new Uri(imageUrl));
                                            await using var fileStream = File.Create(imageFilePath);

                                            await imageStream.CopyToAsync(fileStream);
                                            await imageStream.FlushAsync();
                                            await fileStream.FlushAsync();

                                            imageStream.Close();
                                            fileStream.Close();

                                            return (dynamic)new
                                            {
                                                id = storageFileId
                                            };
                                        }
                                    )
                                );

                                var progress = (double)++finishedListings / listings.Count * 100;

                                if (progress > currentProgress)
                                {
                                    currentProgress = progress;
                                    Console.Clear();
                                    Console.WriteLine($"Progress: {currentProgress:F2}%");
                                }

                                return (dynamic)new
                                {
                                    name = listing.name,
                                    categoryId = listing.categoryId,
                                    imagesStorageFile = images,
                                    location = listing.location,
                                    price = listing.price
                                };
                            }
                        )
                        .ToList()
                )
            );
        }

        return result;
    }
}

public enum Currency
{
    Usd
}