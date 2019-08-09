using System;
using System.Collections.Generic;
using System.Linq;

namespace Year9DataCollection
{


	public interface ISpecification<T>
	{
		bool IsSatisfied(T t);
	}

	public interface IFilter<T>
	{
		IEnumerable<T> filterItems(IEnumerable<T> items, ISpecification<T> spec);
	}

	public class TwoSpecification<T> : ISpecification<T>
	{
		private ISpecification<T> spec1, spec2;

		public TwoSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
		{
			this.spec1 = spec1;
			this.spec2 = spec2;
		}

		public bool IsSatisfied(T t)
		{
			return spec1.IsSatisfied(t) && spec2.IsSatisfied(t);
		}
	}

	public class ClassSpecification : ISpecification<Student>
	{
		private static Dictionary<string, string> Classes = new Dictionary<string, string>
		{
			// (Key, Subject)
			{"Ma2aPHM", "Ad Maths"}, {"Ma1bPHD", "Ad Maths"}, {"Cp", "Computer Science"}, {"Ma", "Maths"}, {"Pe", "PE"}, {"Bi", "Biology"}, {"Ch", "Chemistry"},
			{"En", "English"}, {"Gg", "Geography"}, {"Hi", "History"}, {"PhSML", "Physics"},
			{"PhMKC", "Physics"}, {"PhJUP", "Physics"}, {"PhHI", "Physics"}, {"PhPAH", "Physics"}, {"PhNET", "Physics"}, {"PhSTM", "Physics"}, {"PhDAM", "Physics"},
			{"Ph2", "Physics"}, {"Ph2a", "Physics"}, {"Ph2b", "Physics"},
			{"Ph1", "Physics"}, {"Ph1a", "Physics"}, {"Ph1b", "Physics"},
			{"Ph3", "Physics"}, {"Ph3a", "Physics"},
			{"Fr1", "French"}, {"Fr2", "French"}, {"Fr3", "French"}, {"Cn", "Chinese"},
			{"Dr", "Drama"}, {"Mu", "Music"}, {"Mo1", "Malay"}, {"Mo2", "Malay"}, {"Sp", "Spanish"},
			{"Dp", "Dt Product"}, {"Df", "Dt Food"}, {"Ad", "Art"}, {"10T", "Triple Science"},
			{"Ec", "Economics"}, {"Bs", "Business"}, {"Ms", "Media Studies"}, {"It", "ICT"}, {"11T", "Triple Science"}, {"Ac", "Accounting"}, {"M+", "Further Maths"}, {"Mt", "Music Technology"}, {"Py", "Psychology"}, {"El", "English"}, {"Es", "Environmental Systems and Societies"}, {"My", "Malay"}
		};

		public static Dictionary<string, string> Classes1
		{
			get => Classes;
		}


		// Class we want to find.
		public string subjectName;

		public ClassSpecification(string subjectName)
		{
			foreach (var classPair in Classes)
			{
				if (subjectName == classPair.Value)
				{
					this.subjectName = subjectName;
				}
			}

			if (string.IsNullOrEmpty(subjectName))
			{
				Console.WriteLine("Invalid Subject Name!");
				this.subjectName = "Invalid";
			}
		}

		public bool IsSatisfied(Student t)
		{
			var studentClasses = t.classes;

			foreach (var classes in studentClasses)
			{
				var tempClass = classes.name;

				foreach (var classPair in Classes)
				{
					if (tempClass.Contains(classPair.Key))
					{
						if (classPair.Value == subjectName)
						{
							return true;
						}
					}
				}
			}

			return false;
		}
	}

	public class GenderSpecification : ISpecification<Student>
	{
		private static Dictionary<string, string> genderHouses = new Dictionary<string, string>
		{
			{"Blackeye", "Girl"}, {"Argus", "Boy"}, {"Skylark", "Girl"}, {"Frogmouth", "Boy"}, {"Heron", "Girl"}, {"Swift", "Boy"}, {"Ibis", "Boy"},
			{"Whistler", "Girl"}, {"Eagle", "Boy"}, {"Kingfisher", "Girl"}, {"Hawk", "Boy"}, {"Monarch", "Boy"}, {"Barbet", "Boy"},
			{"Sandpiper", "Girl"}, {"Fireback", "Girl"}, {"Osprey", "Girl"}
		};

		private string gender;

		public GenderSpecification(string gender)
		{
			this.gender = gender;
		}

		public bool IsSatisfied(Student t)
		{
			foreach (var genderPair in genderHouses)
			{
				if (StudentMethods.GetHouse(t) == genderPair.Key)
				{
					return genderPair.Value == gender;
				}
			}

			return false;
		}
	}

	public class BetterFilter : IFilter<Student>
	{
		public IEnumerable<Student> filterItems(IEnumerable<Student> students,
			ISpecification<Student> spec)
		{

			var people = from student in students
				where spec.IsSatisfied(student)
				orderby student.Name ascending
				select student;

			return people;
		}
	}

	public interface ISorting<T>
	{
		IEnumerable<T> SortItems();
	}

}