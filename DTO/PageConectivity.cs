using System;
using System.Xml.Serialization;

namespace Brandman.UITests.DTO {
	[Serializable]
	public class PageConectivity {
		[XmlAttribute("key")]
		public string key;
		[XmlAttribute("value")]
		public string value;
	}
}