using numl.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MLLibraiesBenchmark.NumlBenchmark
{
    public class Iris
    {
        [Feature]
        public decimal SepalLength { get; set; }
        [Feature]
        public decimal SepalWidth { get; set; }
        [Feature]
        public decimal PetalLength { get; set; }
        [Feature]
        public decimal PetalWidth { get; set; }
        [StringLabel]
        public string Class { get; set; }

        public static IEnumerable<Iris> LoadData(string path)
        {
            // Getting en-US culture to parse dot-separated decimal
            CultureInfo usCulture = new CultureInfo("en-US");
            NumberFormatInfo dbNumberFormat = usCulture.NumberFormat;

            var dataLines = File.ReadAllLines(path).Skip(1);
            foreach (var line in dataLines)
            {
                var data = line.Split(',');

                yield return new Iris
                {
                    SepalLength = decimal.Parse(data[0], dbNumberFormat),
                    SepalWidth = decimal.Parse(data[1], dbNumberFormat),
                    PetalLength = decimal.Parse(data[2], dbNumberFormat),
                    PetalWidth = decimal.Parse(data[3], dbNumberFormat),
                    // To simplify results compare: result strings are in uppercase and without any punctuation.
                    Class = data[4].ToUpper().Replace("-", "")
                };
            }
        }
    }
}
