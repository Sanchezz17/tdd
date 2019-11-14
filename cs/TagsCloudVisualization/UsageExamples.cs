using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public static class UsageExamples
    {
        private const string FontFamilyName = "Arial";

        public static void GenerateFirstTagCloud()
        {
            GenerateTagCloud(
                GetTags(25, a => $"word{a.ToString()}", a => a * 3), 
                "first");
        }

        public static void GenerateSecondTagCloud()
        {
            GenerateTagCloud(
                GetTags(50, a => a.ToString(), a => a * 2), 
                "second");
        }

        public static void GenerateThirdTagCloud()
        {
            GenerateTagCloud(
                GetTags(75, a => "25", a => 30),
                "third");
        }

        public static void GenerateFourthTagCloud()
        {
            GenerateTagCloud(
                GetTags(70, a => Math.Pow(5, a).ToString(), a => 10),
                "fourth");
        }
        
        private static void GenerateTagCloud(IEnumerable<TagInfo> tags, string filename)
        {
            var cloudDrawer = new RectangleCloudDrawer(Color.Teal, Brushes.Peru, Brushes.Black);
            cloudDrawer.DrawCloud(tags.ToList(), Environment.CurrentDirectory + $"\\Examples\\{filename}.png");
        }

        private static IEnumerable<TagInfo> GetTags(
            int count,
            Func<int, string> wordGenerator,
            Func<int, int> fontSizeGenerator)
        {
            var layouter = new SpiralRectangleCloudLayouter(new ArchimedeanSpiral(1, 0.05f));
            for (var i = 1; i <= count; i++)
            {
                var font = new Font(FontFamilyName, fontSizeGenerator(i));
                var value = wordGenerator(i);
                var rectSize = TextRenderer.MeasureText(value, font);
                var rect = layouter.PutNextRectangle(rectSize);
                yield return new TagInfo(value, font, rect);
            }
        }
    }
}