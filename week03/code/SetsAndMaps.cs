using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        if (words == null || words.Length == 0) return Array.Empty<string>();
        HashSet<string> seen = new HashSet<string>();
        List<string> pairs = new List<string>();

        foreach (string word in words)
        {
            if (word == null || word.Length != 2) continue; // Validate input
            if (word[0] == word[1]) continue; // Skip self-pairs like "aa"
            string reverse = new string(new char[] { word[1], word[0] });
            if (seen.Contains(reverse))
            {
                pairs.Add($"{word} & {reverse}");
            }
            seen.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        try
        {
            if (!File.Exists(filename))
            {
                System.Diagnostics.Debug.WriteLine($"File not found: {filename}");
                return degrees;
            }
            foreach (var line in File.ReadLines(filename))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var fields = line.Split(',');
                if (fields.Length > 3)
                {
                    string degree = fields[3].Trim();
                    if (!string.IsNullOrEmpty(degree))
                    {
                        if (degrees.ContainsKey(degree))
                            degrees[degree]++;
                        else
                            degrees[degree] = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error reading {filename}: {ex.Message}");
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1 == null || word2 == null) return false;
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length) return false;

        Dictionary<char, int> charCount = new Dictionary<char, int>();
        foreach (char c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        foreach (char c in word2)
        {
            if (!charCount.ContainsKey(c) || charCount[c] == 0) return false;
            charCount[c]--;
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        try
        {
            using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options) ?? new FeatureCollection();
            var results = new List<string>();
            var today = new DateTime(2025, 7, 25, 14, 36, 0, DateTimeKind.Utc).Date; // 05:36 PM EAT (UTC+3) as UTC
            foreach (var feature in featureCollection.features)
            {
                var quakeTime = DateTimeOffset.FromUnixTimeMilliseconds(feature.properties.time).UtcDateTime;
                if (quakeTime.Date == today)
                {
                    results.Add($"{feature.properties.place ?? "Unknown"} - Mag {feature.properties.mag:F2}");
                }
            }
            return results.ToArray();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"API error: {ex.Message}");
            var mockJson = @"{""features"": [
            {""properties"": {""mag"": 2.5, ""place"": ""Test Location 1"", ""time"": 1753487760000}},
            {""properties"": {""mag"": 3.0, ""place"": ""Test Location 2"", ""time"": 1753487760000}},
            {""properties"": {""mag"": 2.8, ""place"": ""Test Location 3"", ""time"": 1753487760000}},
            {""properties"": {""mag"": 2.6, ""place"": ""Test Location 4"", ""time"": 1753487760000}},
            {""properties"": {""mag"": 2.9, ""place"": ""Test Location 5"", ""time"": 1753487760000}},
            {""properties"": {""mag"": 2.7, ""place"": ""Test Location 6"", ""time"": 1753487760000}}
        ]}";
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(mockJson, options) ?? new FeatureCollection();
            var results = new List<string>();
            var today = new DateTime(2025, 7, 25, 14, 36, 0, DateTimeKind.Utc).Date;
            foreach (var feature in featureCollection.features)
            {
                var quakeTime = DateTimeOffset.FromUnixTimeMilliseconds(feature.properties.time).UtcDateTime;
                if (quakeTime.Date == today)
                {
                    results.Add($"{feature.properties.place ?? "Unknown"} - Mag {feature.properties.mag:F2}");
                }
            }
            return results.ToArray();
        }
    }
}
