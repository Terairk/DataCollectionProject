using System;
using System.Collections.Generic;

namespace Year9DataCollection
{
	[Serializable]
	public class Class
	{
		public string name { get; set; }
	}

	[Serializable]
	public class Parent
	{
		public string name { get; set; }
	}

	[Serializable]
	public class Student
	{
		public List<Parent> parents { get; set; }
		public string Name { get; set; }
		public List<Class> classes { get; set; }
	}

	[Serializable]
	public class RootObject
	{
		public List<Student> Student { get; set; }
	}
}