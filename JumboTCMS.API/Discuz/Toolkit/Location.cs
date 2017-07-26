﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JumboTCMS.API.Discuz.Toolkit
{
    ///E:/swf/ <summary>
    ///E:/swf/ 区域信息类
    ///E:/swf/ </summary>
    public class Location
    {
        [XmlElement("street")]
        public string Street;

        [XmlElement("city")]
        public string City;

        [XmlElement("state")]
        public string State;

        [XmlElement("country")]
        public string Country;

        [XmlElement("zip")]
        public string Zip;
    }
}
