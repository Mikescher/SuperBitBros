using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SuperBitBros.OpenRasterFormat {

    public class OpenRasterImage {
        public List<OpenRasterLayer> layers = new List<OpenRasterLayer>();

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public OpenRasterImage(byte[] file) {
            MemoryStream ms = new MemoryStream(file);
            ms.Write(file, 0, file.Length);
            ZipFile zfile = new ZipFile(ms);

            Load(zfile);

            zfile.Close();
            ms.Dispose();
        }

        private string GetAttribute(XmlElement element, string attribute, string defValue) {
            string ret = element.GetAttribute(attribute);
            return string.IsNullOrEmpty(ret) ? defValue : ret;
        }

        private void Load(ZipFile file) {
            XmlDocument stackXml = new XmlDocument();

            stackXml.Load(file.GetInputStream(file.GetEntry("stack.xml")));

            XmlElement imageElement = stackXml.DocumentElement;
            Width = int.Parse(imageElement.GetAttribute("w"));
            Height = int.Parse(imageElement.GetAttribute("h"));

            XmlElement stackElement = (XmlElement)stackXml.GetElementsByTagName("stack")[0];
            XmlNodeList layerElements = stackElement.GetElementsByTagName("layer");

            if (layerElements.Count == 0)
                throw new XmlException("No layers found in OpenRaster file");

            for (int i = 0; i < layerElements.Count; i++) {
                XmlElement layerElement = (XmlElement)layerElements[i];
                int x = int.Parse(GetAttribute(layerElement, "x", "0"));
                int y = int.Parse(GetAttribute(layerElement, "y", "0"));
                string name = GetAttribute(layerElement, "name", string.Format("Layer {0}", i));
                string path = layerElement.GetAttribute("src");

                ZipEntry zf = file.GetEntry(path);
                Stream s = file.GetInputStream(zf);
                Image img = Image.FromStream(s);
                s.Dispose();

                layers.Add(new OpenRasterLayer(img, name, x, y));
            }
        }

        public OpenRasterLayer GetLayer(string name) {
            return layers.Find(x => (x.Name == name));
        }

        public Color GetColor(string layer, int x, int y) {
            if (x < 0 || y < 0 || x > Width || y > Height)
                throw new ArgumentException(String.Format("Coordinates Out of Range ({0} | {1})", x, y));

            OpenRasterLayer orl = GetLayer(layer);
            if (orl == null)
                throw new ArgumentException(String.Format("Layer not found ({0})", layer));

            return orl.GetColor(x, y);
        }
    }
}