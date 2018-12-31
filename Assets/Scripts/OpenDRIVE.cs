using System;
using System.Xml.Serialization;

[Serializable]
public class OpenDRIVE
{
	[XmlElement("header")]
	public Header header; 
	[XmlElement("road")]
	public Road[] roads;

	[XmlElement("controller")]
	public Controller[] controllers;
	[XmlElement("junction")]
	public Junction[] junctions;
}

[Serializable]
public class Junction
{
	[XmlAttribute]
	public int id;
	[XmlAttribute]
	public string name;
	[XmlElement("connection")]
	public Connection[] connection;
}
[Serializable]
public class Connection
{
	[XmlAttribute]
	public int id;
	[XmlAttribute]
	public int incomingRoad;
	[XmlAttribute]
	public int connectingRoad;
	[XmlAttribute]
	public string contactPoint;
	[XmlElement("laneLink")]
	public LaneLink laneLink;
}

[Serializable]
public class LaneLink
{
	[XmlAttribute]
	public int from;
	[XmlAttribute]
	public int to;
}

[Serializable]
public class Controller
{
	[XmlAttribute]
	public string id;
	[XmlElement("control")]
	public Control[] control;
}
[Serializable]
public class Control
{
	[XmlAttribute]
	public string signalId;
	[XmlAttribute]
	public string type;
}



[Serializable]
public class Header
{
	[XmlAttribute]
	public int revMajor;
	[XmlAttribute]
	public int revMinor;
	[XmlAttribute]
	public string name;
	[XmlAttribute]
	public float version;
	[XmlAttribute]
	public string date;
	[XmlAttribute]
	public float north;
	[XmlAttribute]
	public float south;
	[XmlAttribute]
	public float east;
	[XmlAttribute]
	public float west;
}

[Serializable]
public class Road
{
	[XmlAttribute]
	public string name;
	[XmlAttribute]
	public float length;
	[XmlAttribute]
	public int id;
	[XmlAttribute]
	public int junction;
	[XmlElement("link")]
	public Link link;
	[XmlElement("planView")]
	public PlainView plainView;
	[XmlElement("lanes")]
	public Lanes lanes;
	[XmlElement("signals")]
	public Signals signals;
}
[Serializable]
public class Signals
{
	[XmlElement("signal")]
	public Signal signal;
}

[Serializable]
public class Signal
{
	[XmlAttribute]
	public int s;
	[XmlAttribute]
	public string id;
}

[Serializable]
public class Link
{
	[XmlElement("predecessor")]
	public Predecessor predecessor;

	[XmlElement("successor")]
	public Successor successor;
}

[Serializable]
public class Predecessor
{
	[XmlAttribute]
	public string elementType;
	[XmlAttribute]
	public int elementId;
	[XmlAttribute]
	public string contactPoint;
}

[Serializable]
public class Successor
{
	[XmlAttribute]
	public string elementType;
	[XmlAttribute]
	public int elementId;
	[XmlAttribute]
	public string contactPoint;
}

[Serializable]
public class PlainView
{
	[XmlElement("geometry")]
	public Geometry geometry;
}

[Serializable]
public class Geometry
{
	[XmlElement("arc")]
	public Arc arc;
	[XmlAttribute]
	public float s;
	[XmlAttribute]
	public float x;
	[XmlAttribute]
	public float y;
	[XmlAttribute]
	public float hdg;
	[XmlAttribute]
	public float length;
}

[Serializable]
public class Arc
{
	[XmlAttribute]
	public float curvature;
}

[Serializable]
public class Lanes
{
	[XmlElement("laneSection")]
	public LaneSection laneSection;
}

[Serializable]
public class LaneSection
{
	[XmlAttribute]
	public float s;
	[XmlElement("right")]
	public Right right;
}

[Serializable]
public class Right
{
	[XmlElement("lane")]
	public Lane[] lane;
}

[Serializable]
public class Lane
{
	[XmlAttribute]
	public int id;
	[XmlAttribute]
	public string type;
	[XmlAttribute]
	public bool level;
	[XmlElement("width")]
	public Width width;
}

[Serializable]
public class Width
{
	[XmlAttribute]
	public float sOffset;
	[XmlAttribute]
	public float a;
	[XmlAttribute]
	public float b;
	[XmlAttribute]
	public float c;
	[XmlAttribute]
	public float d;
}


