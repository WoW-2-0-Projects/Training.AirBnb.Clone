using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Parser.Services;

public static class PrepareListingService
{
    public static async ValueTask PrepareAsync()
    {
        var workingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");

        var enabledCategories = new List<Guid>
        {
            Guid.Parse("3d7ee634-bbfe-461b-a2d5-21e7e428fc3e"),
            Guid.Parse("0b8898ba-9f0e-49f3-b4e2-d07fade4ae4e"),
            Guid.Parse("5bdab5b8-a91d-46bf-b3d2-d9390292c0f0"),
            Guid.Parse("81cec3a8-a16f-483d-9732-0e151bfd0de4"),
            Guid.Parse("81cec3a8-a16f-483d-9732-0e151bfd0de4"),
            Guid.Parse("79912f1b-f762-420b-9eaa-e72eda426e69"),
            Guid.Parse("9a9dc655-5e86-44ed-bab6-025cbfa22c39"),
            Guid.Parse("6768b598-4187-4386-a778-6ed3bcbc8900"),
            Guid.Parse("f5255134-9aea-42f0-b6c4-eb3668111273"),
            Guid.Parse("da5fe355-2b2c-4c53-a691-60c886712eb6"),
        };

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
            .Where(directory => enabledCategories.Contains(directory.CategoryId))
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
                        throw new Exception("Less than 5 images for listing - " + listings.First(listing => listing.imagesStorageFile.Count < 5).name);

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

    private static async ValueTask<List<dynamic>> ExecuteAsync(IEnumerable<dynamic> listings)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://a0.muscache.com/im/pictures/")
        };

        httpClient.Timeout = TimeSpan.FromMinutes(20);

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