using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Brandman.UITests.DTO {
	[Serializable]
	public class ListUrlConectivity {
		[XmlElement("PageConectivity")]
		public PageConectivity[] Pages { get; set; }

		[XmlIgnore]
		public List<PageConectivity> Items => new List<PageConectivity>(Pages);
	}
}