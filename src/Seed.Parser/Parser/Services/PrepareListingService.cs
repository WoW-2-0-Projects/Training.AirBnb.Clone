using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Parser.Services;

public static class PrepareListingService
{
    public static async ValueTask PrepareAsync()
    {
        var workingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");

        // Retrieve listing categories
        var listingCategoriesFileName = Path.Combine(workingDirectory, "Data", "SeedData", "ListingCategories.json");
        var listingCategories = JsonConvert.DeserializeObject<List<dynamic>>(await File.ReadAllTextAsync(listingCategoriesFileName))!;

        // Get all listing folders
        var directories = Directory.GetDirectories(Path.Combine(workingDirectory, "Data", "SeedData", "Prepare"))
            .Select(directory => new DirectoryInfo(directory))
            .ToList();

        var listingFiles = directories.Select(
                directory =>
                {
                    return (
                        CategoryId: Guid.Parse(
                            listingCategories.First(
                                    category => category.name.ToString().Contains(directory.Name, StringComparison.InvariantCultureIgnoreCase)
                                )
                                .id.ToString()
                        ), Files: Directory.GetFiles(directory.FullName, "*.json").ToList());
                }
            )
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
                                            .Take(10)
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
                                            amount = decimal.Parse(
                                                result["pricingQuote"]!["structuredStayDisplayPrice"]!["primaryLine"]!["price"]!.Value<string>()!
                                                    .Replace(
                                                        "$",
                                                        ""
                                                    )
                                            )
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

                    return listings.Take(100).ToList();
                }
            )
            .ToList();

        var listingsResult = await ExecuteAsync(listings);

        // Save listing to file

        var listingsFileName = Path.Combine(workingDirectory, "Data", "SeedData", "Listings.json");

        await File.WriteAllTextAsync(listingsFileName, JsonConvert.SerializeObject(listingsResult, Formatting.Indented));
    }

    private static async ValueTask<List<dynamic>> ExecuteAsync(IEnumerable<dynamic> listings)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://a0.muscache.com/im/pictures/")
        };

        var workingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");

        var listingsResult = await Task.WhenAll(
            listings.Select(
                    async listing =>
                    {
                        var images = await Task.WhenAll(
                            ((List<string>)listing.imagesStorageFile).Select(
                                async imageUrl =>
                                {
                                    var storageFileId = Guid.NewGuid();
                                    var imageFileName = $"{storageFileId}.jpg";
                                    var imageFilePath = Path.Combine(workingDirectory, "Data", "SeedData", "Images", "Listings", imageFileName);

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
        );

        return listingsResult.ToList();

        // Save listing to file
    }
}

public enum Currency
{
    Usd
}