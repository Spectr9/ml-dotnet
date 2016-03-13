using numl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
