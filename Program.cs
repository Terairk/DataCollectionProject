using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using ServiceStack.Text;
using static Year9DataCollection.StudentMethods;

namespace Year9DataCollection
{
	class Program
	{
		static void Main(string[] args)
		{
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

			Console.Write("Type a year to get data from: (Example: 2018, 2019, 2020) \n Input: ");
			var yearSelected = Console.ReadLine();

			if (yearSelected != "2018")
			{
				Console.WriteLine("Invalid Year. Valid years so far are: \n 2018");
				Console.WriteLine("Program closing soon. Reason: Invalid Input.");
				System.Threading.Thread.Sleep(2000);
				return;
			}

			string[] pathsY7 = {@"Data", yearSelected, @"studentsY7.json"};
			string[] pathsY8 = {@"Data", yearSelected, @"studentY8.json"};
			string[] pathsY9 = {@"Data", yearSelected, @"studentY9.json"};
			string[] pathsY10 = {@"Data", yearSelected, @"studentY10.json"};
			string[] pathsY11 = {@"Data", yearSelected, @"studentY11.json"};
			string[] pathsY12 = {@"Data", yearSelected, @"studentY12.json"};
			string[] pathsY13 = {@"Data", yearSelected, @"studentY13.json"};

			string pathToStudentsY7 = Path.Combine(pathsY7);
			string pathToStudentsY8 = Path.Combine(pathsY8);
			string pathToStudentsY9 = Path.Combine(pathsY9);
			string pathToStudentsY10 = Path.Combine(pathsY10);
			string pathToStudentsY11 = Path.Combine(pathsY11);
			string pathToStudentsY12 = Path.Combine(pathsY12);
			string pathToStudentsY13 = Path.Combine(pathsY13);
			var pathToStatistics = @"Data\statistics.txt";

			var studentsY7 = ConvertJsonToStudents(pathToStudentsY7);
			var studentsY8 = ConvertJsonToStudents(pathToStudentsY8);
			var studentsY9 = ConvertJsonToStudents(pathToStudentsY9);
			var studentsY10 = ConvertJsonToStudents(pathToStudentsY10);
			var studentsY11 = ConvertJsonToStudents(pathToStudentsY11);
			var studentsY12 = ConvertJsonToStudents(pathToStudentsY12);
			var studentsY13= ConvertJsonToStudents(pathToStudentsY13);

			var sw = new StreamWriter(pathToStatistics, true);
			sw.AutoFlush = true;


			List<Student> allStudents2018 = new List<Student>();
			allStudents2018 = SortStudents(allStudents2018).ToList();
			allStudents2018.AddRange(studentsY7);
			allStudents2018.AddRange(studentsY8);
			allStudents2018.AddRange(studentsY9);
			allStudents2018.AddRange(studentsY10);
			allStudents2018.AddRange(studentsY11);
			allStudents2018.AddRange(studentsY12);
			allStudents2018.AddRange(studentsY13);

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("IMPORTANT - PLEASE READ!");
			Console.WriteLine("When selecting option 1, you can type 'all' to " +
			                  "select all year groups");
			Console.WriteLine("When selecting option 2, it analyzes subjects based on " +
			                  "the year group selected");
			Console.WriteLine("When selecting option 5, it lists everyone in a house" +
			                  "based on the year group selected");

			Console.ForegroundColor = ConsoleColor.Magenta;
			var subjects = GetSubjects();
			subjects = SortItems(subjects).ToList();
			string strSubjects = string.Join(", ", subjects);
			Console.WriteLine("Subject choices are: \n {0} \n", strSubjects );

			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Are you ready to begin? If so, press enter to begin. ");
			var secret = Console.ReadLine();
			var secretBool = false;

			if (!(secret == String.Empty))
			{
				secretBool = true;
			}

			bool flag = true;
			string yearLevel = "9";
			Student currstudent = new Student();
			List<Student> students = new List<Student>();
			students = allStudents2018;
			students = SortStudents(students).ToList();
			PrintMessage();

			while (flag)
			{
				Console.Write("Enter a number: ");
				string input = Console.ReadLine();
				students = SortStudents(students).ToList();

				if (input == "0")
				{
					PrintMessage();
				}
				else if (input == "1")
				{
					Console.Write("  Enter in a year group (7-13): ");
					yearLevel = Console.ReadLine().ToLower();

					if (yearLevel == "all")
					{
						Console.WriteLine("  Selected all year groups.");
						students = allStudents2018;
					}
					else
					{
						if (yearLevel == "7")
						{
							students = studentsY7;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						}
						else if (yearLevel == "8")
						{
							students = studentsY8;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						}
						else if (yearLevel == "9")
						{
							students = studentsY9;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						} else if (yearLevel == "10")
						{
							students = studentsY10;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						} else if (yearLevel == "11")
						{
							students = studentsY11;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						}
						else if (yearLevel == "12")
						{
							students = studentsY12;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						}
						else if (yearLevel == "13")
						{
							students = studentsY13;
							Console.WriteLine($"  Selected Year {yearLevel}.");
						} else if (yearLevel == "middle years")
						{
							students = studentsY7;
							students.AddRange(studentsY8);
							students.AddRange(studentsY9);
							Console.WriteLine("  Selected Year 7,8 & 9.");
						} else if (yearLevel == "gcse")
						{
							students = studentsY10;
							students.AddRange(studentsY11);
							Console.WriteLine("Selected Year 10 & 11.");
						} else if (yearLevel == "sixth form")
						{
							students = studentsY12;
							students.AddRange(studentsY13);
							Console.WriteLine("Selected Year 12 & 13.");
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("  Invalid Input for year group!");
							Console.WriteLine("  Default is now: All students.");
							students = allStudents2018;
						}
					}
				}
				else if (input == "2")
				{
					Console.Write("  Enter the subject to analyze: ");
					var subject = Console.ReadLine();

					if (subject != null)
					{
						subject = textInfo.ToTitleCase(subject.ToLower());
					}

					if (yearLevel == "all")
					{
						Console.WriteLine($"  Analyzing {subject} for all year groups");
					}
					else
					{
						Console.WriteLine($"  Analyzing {subject} for year {yearLevel}");
					}
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Write("  Press Enter when you're ready for the data.");
					Console.ReadLine();

					var dataWrite = analyzeData(students, subject, yearLevel, out string message);

					if (message == "Invalid Input")
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid Input, Try again with a valid input.");
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.WriteLine("Subject choices are: \n {0} \n", strSubjects);
					}
					else
					{
						if (secretBool)
						{
							sw.WriteLine(dataWrite);
						}
					}

				}
				else if (input == "3")
				{
					Console.Write("  Enter in the first name of the person you want: ");
					var firstName = Console.ReadLine();

					Console.Write("  Enter in the year level they're in: ");
					string stryeargroup = Console.ReadLine();
					var intYearGroup = 0;


					if (stryeargroup != null && new[] {"7", "8", "9", "10", "11", "12", "13"}.Contains(stryeargroup))
					{
						intYearGroup = Convert.ToInt32(stryeargroup);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid Year Group!");
						Console.ForegroundColor = ConsoleColor.White;
						continue;
					}

					if (!string.IsNullOrEmpty(firstName))
					{
						firstName = firstName.ToLower();

						if (string.IsNullOrEmpty(stryeargroup))
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("Year group can't be nothing!");
							Console.ForegroundColor = ConsoleColor.White;
							continue;
						}

						currstudent = GetStudent(allStudents2018, firstName, intYearGroup);

						if (currstudent != null)
						{
							Console.ForegroundColor = ConsoleColor.Magenta;
							Console.WriteLine($"  Selected {currstudent.Name}.");
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine($"  There is no student in Year {intYearGroup} who has the first name of {firstName}");
						}

					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("  Invalid Name!");
					}
				}
				else if (input == "4")
				{
					if (!string.IsNullOrEmpty(currstudent.Name))
					{
						Console.Write($"  Press Enter when you're ready to see the options of {currstudent.Name}:");
						Console.ReadLine();
						var options = GetOptions(currstudent);
						options = SortItems(options);

						Console.ForegroundColor = ConsoleColor.Green;
						var swString1 = $"{currstudent.Name} is in Year {GetYearLevel(currstudent)} and is in {GetHouse(currstudent)}.\n";
						Console.WriteLine($"    {currstudent.Name} is in Year {GetYearLevel(currstudent)} and is in {GetHouse(currstudent)}.");

						Console.ForegroundColor = ConsoleColor.Cyan;

						foreach (var subject in options)
						{
							Console.WriteLine($"    {currstudent.Name} takes {subject}");
						}

						var swString2 = $"{currstudent.Name} takes {string.Join(", ", options)}. \n";

						Console.ForegroundColor = ConsoleColor.Red;
						var parent1Name = currstudent.parents[0].name;

						if (parent1Name != null)
						{
							StringBuilder stringBuilder = new StringBuilder();

							foreach (var student in allStudents2018)
							{

								try
								{


									if (student.parents[0].name == parent1Name)
									{
										if (student.Name == currstudent.Name)
										{

										}
										else
										{
											Console.WriteLine($"    {student.Name} is {currstudent.Name}'s sibling");
											var swString3 = $"{student.Name} is {currstudent.Name}'s sibling and is in Year {GetYearLevel(student)}.";
											stringBuilder.Append(swString3);
										}
									}

								}
								catch (ArgumentOutOfRangeException)
								{

								}
							}

							var swString4 = stringBuilder.ToString();

							var swString5 = "\n";

							var swString6 = "=================================================================" +
							                "======================================================== \n";
							var combinedString = swString1 + swString2 + swString4 + swString5 + swString6;

							if (secretBool)
							{
								sw.WriteLine(combinedString);
							}
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine($"{currstudent.Name} seems not to have parents registered on firely!");

						}

					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("There is no student currently selected!");
					}


				}
				else if (input == "5")
				{
					Console.Write("  Enter in a house: ");
					var house = Console.ReadLine();

					if (house == null)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid house name!");
						continue;
					}

					Console.Write("  Press Enter when you're ready to see the data.");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.ReadLine();
					students = SortStudents(students).ToList();

					foreach (var student in students)
					{
						if (GetHouse(student).ToLower() == house.ToLower())
						{
							Console.WriteLine($"    {student.Name} is in {house}.");
						}
					}
				}
				else if (input == "6")
				{
					Console.Write("  Hit Enter when you're ready to see the students: ");
					Console.ReadLine();
					ListStudents(students);

				}
				else if (input == "7")
				{
					Console.Write("  Enter the full name: ");
					var fullName = Console.ReadLine();

					if (fullName == null)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Invalid Full Name!");
						continue;
					}

					fullName = textInfo.ToTitleCase(fullName.ToLower());
					var student = GetStudent(allStudents2018, fullName);

					if (student != null)
					{
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine($"  {student.Name} is in Year {GetYearLevel(student)}");
						currstudent = student;
						Console.WriteLine($"  Selected {currstudent.Name}.");
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"  Couldn't find {fullName} in current database.");
					}

				}
				else if (input == "8")
				{
					flag = false;
				}
				else
				{
					Console.WriteLine("Invalid Input.");
				}

				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("==========================================================" +
				                  "============================");
			}

			studentsY9 = SortStudents(studentsY9).ToList();
			foreach (var student in studentsY9)
			{
				Console.WriteLine(student.Name);
			}
			Console.ReadLine();

		}

		public static void PrintMessage()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Type in a number for the functionality that you want!");
			Console.WriteLine("0 - List options again.");
			Console.WriteLine("1 - Select Year Level.");
			Console.WriteLine("2 - Select Subject to analyze.");
			Console.WriteLine("3 - Select a person. ");
			Console.WriteLine("4 - Get information about the person selected.");
			Console.WriteLine("5 - List everyone in a house.");
			Console.WriteLine("6 - List everyone in the year selected.");
			Console.WriteLine("7 - Get year group of Full Name and select them.");
			Console.WriteLine("8 - Quit Program");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static List<Student> ConvertJsonToStudents(string path)
		{
			StreamReader r = new StreamReader(path);
			var json = r.ReadToEnd();
			var results = JsonSerializer.DeserializeFromString<RootObject>(json);
			var students = results.Student;
			students = SortStudents(students).ToList();

			return students;

		}
	}
}