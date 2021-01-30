using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ExtractGPXData
{
    public class Program
    {
        public static void Main(string[] args)
        
        {
            while (true)
            {
                XDocument gpx = ImportGPX();

                // Load namespaces
                XNamespace gpxNamespace = XNamespace.Get("http://www.topografix.com/GPX/1/1");
                XNamespace gpxExtNamespace = XNamespace.Get("http://www.garmin.com/xmlschemas/TrackPointExtension/v1");

                // Get Data that contains temperature only
                List<GPXViewModel> data = (from ele in gpx.Descendants(gpxNamespace + "trkpt")
                                           where ele.Element(gpxNamespace + "extensions") != null
                                           select new GPXViewModel()
                                           {
                                               latitude = Convert.ToDouble(ele.Attribute("lat").Value),
                                               longitude = Convert.ToDouble(ele.Attribute("lat").Value),
                                               time = Convert.ToDateTime(ele.Element(gpxNamespace + "time").Value),
                                               elevation = Convert.ToDouble(ele.Element(gpxNamespace + "ele").Value),
                                               temperature = Convert.ToDouble(ele.Element(gpxNamespace + "extensions").Element(gpxExtNamespace + "TrackPointExtension").Element(gpxExtNamespace + "atemp").Value)
                                           }
                   ).ToList();

                Console.WriteLine("Trackpoint Count:" + " " + data.Count);
                Console.WriteLine("");

                // Specify Output Folder i.e. D:\Karim\OneDrive\Desktop\Output
                Console.Write("2. Where do you want to save the exported file? (i.e. D:/Main/Output): ");
                string _out = Console.ReadLine();
                Console.WriteLine("");

                // Write Data Real
                CreateCsv(data, _out + "/exported.txt");

                // Write Data
                foreach (var item in data)
                {
                    Console.WriteLine(item.latitude + "," + item.longitude + "," + item.time + "," + item.elevation + "," + item.temperature);
                }

                // Trackpoint 
                Console.WriteLine("Conversion finished");
                Console.WriteLine();
            }
        }

        public static XDocument ImportGPX()
        {
            Console.WriteLine("Garmin Track (GPX) to CSV Converter");
            Console.WriteLine("");

            // Import GPX File
            //Specify File Path i.e. C:/Users/karim/source/repos/ExtractGPXData/ExtractGPXData/Slamet.gpx
            Console.Write("1. Import your .gpx file here (i.e. D:/Main/Input/track.gpx): ");
            string gpxPath = Console.ReadLine();
            Console.WriteLine("");

            // Load Converted XML File
            XDocument gpx = XDocument.Load(gpxPath);

            return gpx;
        }

        public static void CreateCsv(List<GPXViewModel> model, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, false))
                {
                    foreach (var item in model)
                    {
                        file.WriteLine(item.latitude + "," + item.longitude + "," + item.elevation + "," + item.time + "," + item.temperature);
                    }
                }
            }
            catch (Exception err)
            {
                throw new ApplicationException("oh my there's something happened", err);
            }
        }
    }
}
