using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Year9DataCollection
{
	public static class StudentMethods
	{
		private static Dictionary<string, string> houses = new Dictionary<string,string>
		{
			// Added the tutor groups for osprey since it conflicts with everything else without.
			{"BKh", "Blackeye"}, {"AGh", "Argus"}, {"SLh", "Skylark"}, {"FMh", "Frogmouth"}, {"HRh", "Heron"}, {"SFh", "Swift"}, {"IBh", "Ibis"}, {"WLh", "Whistler"}, {"EGh", "Eagle"}, {"KFh", "Kingfisher"}, {"HKh", "Hawk"}, {"MNh", "Monarch"}, {"BBh", "Barbet"}, {"SPh", "Sandpiper"}, {"FBh", "Fireback"}, {"OPhRIG", "Osprey"}, {"OPhCLG", "Osprey"},
			{"BKf", "Blackeye"}, {"AGf", "Argus"}, {"SLf", "Skylark"}, {"FMf", "Frogmouth"}, {"HRf", "Heron"}, {"SFf", "Swift"}, {"IBf", "Ibis"},
			{"WLf", "Whistler"}, {"EGf", "Eagle"}, {"KFf", "Kingfisher"}, {"HKf", "Hawk"}, {"MNf", "Monarch"}, {"BBf", "Barbet"},
			{"SPf", "Sandpiper"}, {"FBf", "Fireback"}, {"OPf", "Osprey"}
		};

		private static Dictionary<string, string> Classes = ClassSpecification.Classes1;

		public static string GetHouse(Student student)
		{
			foreach (var classes in student.classes)
			{
				var tempClass = classes.name;

				foreach (var housePair in houses)
				{
					if (tempClass.Contains(housePair.Key))
					{
						return housePair.Value;
					}
				}

			}

			return "No house";
		}

		public static IEnumerable<Student> SortStudents(IEnumerable<Student> students)
		{
			var sortedStudents = from student in students
				orderby student.Name ascending
				select student;

			return sortedStudents;
		}

		public static IEnumerable<string> SortItems(IEnumerable<string> items)
		{
			var sortedItems = from item in items
				orderby item ascending
				select item;

			return sortedItems;
		}

		public static IEnumerable<string> GetOptions(Student student)
		{
			var classes = student.classes;
			int yeargroup = GetYearLevel(student);
			var options = new List<string>();
			options = SortItems(options).ToList();

			foreach (var subject in classes)
			{
				foreach (var classPair in Classes)
				{
					if (subject.name.Contains(classPair.Key))
					{
						if (yeargroup == 12 || yeargroup == 13)
						{
							options.Add(classPair.Value);
						}
						else if (classPair.Key == "Ma" || classPair.Key == "En" ||
						    classPair.Key == "Bi" || classPair.Value == "Physics" ||
						    classPair.Key == "Ch" || classPair.Key == "Pe")
						{

						}
						else
						{
							if (yeargroup == 7 || yeargroup == 8)
							{
								if (classPair.Key == "Hi" || classPair.Key == "Gg" || classPair.Key == "Dp"
								    || classPair.Key == "Mu" || classPair.Key == "Dr" ||
								    classPair.Key == "Ad" || classPair.Key == "Cp")
								{
									continue;
								}
							}
							if (yeargroup == 9)
							{
								if (classPair.Key == "Hi" || classPair.Key == "Gg")
								{
									continue;
								}
							}

							options.Add(classPair.Value);
						}
					}
				}

				options = options.Union(options).ToList();
			}

			if (options.Contains("Triple Science"))
			{
				options.Insert(0, "Triple Science - Biology");
				options.Insert(0, "Triple Science - Chemistry");
				options.Insert(0, "Triple Science - Physics");
				options.Remove("Triple Science");
				options.Remove("Triple Science");
				options.Remove("Triple Science");
			}
			return options;
		}

		public static Student GetStudent(IEnumerable<Student> students, string fullname)
		{
			foreach (var student in students)
			{
				if (student.Name == fullname)
				{
					return student;
				}
			}

			return null;
		}

		public static Student GetStudent(IEnumerable<Student> students, string firstName,
			int yearLevel)
		{
			foreach (var student in students)
			{
				if (getFirstName(student) == firstName && GetYearLevel(student) == yearLevel)
				{
					return student;
				}
			}

			return null;
		}

		public static int GetYearLevel(Student student)
		{
			foreach (var mclass in student.classes)
			{
				if (mclass.name == "Year 7")
				{
					return 7;
				}
				else if(mclass.name == "Year 8")
				{
					return 8;
				}
				else if (mclass.name == "Year 9")
				{
					return 9;
				}
				else if (mclass.name == "Year 10")
				{
					return 10;
				} else if (mclass.name == "Year 11")
				{
					return 11;
				}
				else if (mclass.name == "Year 12")
				{
					return 12;
				}
				else if (mclass.name == "Year 13")
				{
					return 13;
				}
			}

			return 0;
		}


		private static string getFirstName(Student student)
		{
			var words = student.Name.Split(' ');
			return words[0].ToLower();
		}


		public static string analyzeData(IEnumerable<Student> students, string subject, string yearGroup, out string message)
		{
			var spec1 = new ClassSpecification(subject);

			if (spec1.subjectName == "Invalid")
			{
				message = "Invalid Input";
				return "Invalid Input";
			}

			var bf = new BetterFilter();

			var SubjectStud = bf.filterItems(students, spec1);
			var maleCount = 0;
			var femaleCount = 0;
			var counter = 0;
			var boySpec = new GenderSpecification("Boy");
			var swString1 = $"In year {yearGroup}, People who do {subject} are: \n \n  ";
			StringBuilder stringBuilder = new StringBuilder();

			foreach (var student in SubjectStud)
			{
				string string2 = $"{student.Name}, ";
				if (counter == 6)
				{
					string2 = $"{student.Name} \n  ";
					counter = -1;
				}

				stringBuilder.Append(string2);
				if (boySpec.IsSatisfied(student))
				{
					maleCount++;
				}
				else if (!boySpec.IsSatisfied(student))
				{
					femaleCount++;
				}

				counter++;
			}

			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			var swString2 = stringBuilder.ToString();
			var swString3 = "\n";
			var swString4 = $"    {SubjectStud.Count()} people do {subject}\n";
			var swString5 = $"    {maleCount} boys do {subject}";
			var swString6 = $"    {femaleCount} girls do {subject} \n";
			var swString7 = "========================================================================" +
			                "================================================ \n";


			var semiCompletedString = swString2 + swString3;
			var combinedString = swString1 + swString2 + swString3 + swString4 + swString5 + swString6
				+ swString7;

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(swString1);
			System.Threading.Thread.Sleep(1000);
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(semiCompletedString);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(swString4 + swString5 + swString6);

			message = "Successful";
			return combinedString;

		}

		public static void ListStudents(IEnumerable<Student> students)
		{
			foreach (var student in students)
			{
				var options = GetOptions(student);
				options = SortItems(options).ToList();
				string strOptions = string.Join(", ", options);
				Console.Write($"  {student.Name} does");
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($" the subjects: {strOptions}");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}

		public static IEnumerable<string> GetSubjects()
		{
			var subjects = new List<string>();

			foreach (var subjectPair in Classes)
			{
				subjects.Add(subjectPair.Value);
			}

			subjects = subjects.Union(subjects).ToList();
			subjects = SortItems(subjects).ToList();

			return subjects;
		}

		public static IEnumerable<Student> GetSiblings(IEnumerable<Student> students, Student student)
		{
			var studentParentName1 = student.parents[0].name;
			var siblings = new List<Student>();
			siblings = SortStudents(siblings).ToList();
			foreach (var people in students)
			{
				if (people.parents[0].name == studentParentName1)
				{
					siblings.Add(people);
				} else if (people.parents[1].name == student.parents[1].name)
				{
					siblings.Add(people);
				}
			}

			return siblings;
		}


	}
}