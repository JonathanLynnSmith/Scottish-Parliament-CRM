using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottishParliamentMembers.Repository;

namespace ScottishParliamentMembers
{
    class Program
    {
        static void Main(string[] args)
        {
            ScottishParlimentDBAccess dBAccess = new ScottishParlimentDBAccess();
            Console.WriteLine("   -----------------------------------------------");
            Console.WriteLine("  |                   Main Menu                   |");
            Console.WriteLine("   -----------------------------------------------");
            Console.WriteLine("  |                                               |");
            Console.WriteLine("  |    (1) --Get list of all member's names--     |");
            Console.WriteLine("  |    (2) --Add Member--                         |");
            Console.WriteLine("  |    (3) --Find Member by Id--                  |");
            Console.WriteLine("  |    (4) --Edit Member by Id--                  |");
            Console.WriteLine("  |                                               |");
            Console.WriteLine("   -----------------------------------------------");
            Console.WriteLine("What would you like to do?");
            Console.Write("Option: ");
            string input = Console.ReadLine();
            if (input == "exit") {
                Environment.Exit(0);
            }
            else if (!int.TryParse(input, out int value)) {
                Console.WriteLine();
                Console.WriteLine("Please enter a number only");
                GoBackToMainMenu();
            }
            else
            {
                AssessOptions(Convert.ToInt32(input));
            }



            void AssessOptions(int optionSelected)
            {
                if (optionSelected == 1)
                {
                    var allMembersNames = dBAccess.GetAllMembersNames();
                    Console.WriteLine("All Names in the database:");
                    foreach (var member in allMembersNames)
                    {
                        Console.Write("ID: " + member.PersonID + " |  Name: " + member.ParliamentaryName);
                        Console.WriteLine();
                    }
                    GoBackToMainMenu();
                }
                else if (optionSelected == 2)
                {
                    Member member = RequestMemberInformation();
                    dBAccess.AddMember(member);
                    Console.WriteLine();
                    Console.WriteLine($"{member.PreferredName} has been added as a member");
                    GoBackToMainMenu();
                }
                else if (optionSelected == 3)
                {
                    Member memberInfo = GetMemberById();
                    if(memberInfo != null)
                    {
                        displayMemberInformation(memberInfo);
                        GoBackToMainMenu();
                    } else
                    {
                        GoBackToMainMenu();
                    }
                }
                else if (optionSelected == 4)
                {
                    Member memberInfo = GetMemberById();
                    if (memberInfo != null)
                    {
                        memberInfo = SelectPropertyToEdit(memberInfo);
                        SaveMember(memberInfo);
                        GoBackToMainMenu();
                    }
                    else
                    {
                        GoBackToMainMenu();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("################################");
                    Console.WriteLine("Please Select an avaliable option");
                    Console.WriteLine("################################");
                    Console.WriteLine();
                    GoBackToMainMenu();
                }
            }
            void GoBackToMainMenu()
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                Main(args);
            }
            Member RequestMemberInformation()
            {
                Member member = new Member();
                try
                {
                    Console.Write("Name: ");
                    member.ParliamentaryName = Console.ReadLine();

                    Console.Write("Perferred Name: ");
                    member.PreferredName = Console.ReadLine();

                    Console.Write("BirthDate: ");
                    member.BirthDate = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Should birth date be protected? ");
                    member.BirthDateIsProtected= Convert.ToBoolean(Console.ReadLine());

                    Console.Write("Select Gender (M or F) : ");
                    var gender = Console.ReadLine();
                    member.GenderTypeID = 1;
                    if (gender == "m") { member.GenderTypeID = 2; }

                    Console.Write("Notes: ");
                    member.Notes = Console.ReadLine();

                    Console.Write("Photo URL: ");
                    member.PhotoURL = Console.ReadLine();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex); 
                }



                return member;
            }
            Member GetMemberById()
            {
                Member member = new Member();
                Console.Clear();
                Console.Write("Id: ");
                int id = new int();
                try { id = Convert.ToInt32(Console.ReadLine()); } catch { Console.WriteLine("Exiting.."); GoBackToMainMenu(); }
                try
                {
                    member = dBAccess.GetMemberInfoById(id);
                    return member;
                }
                catch
                {
                    Console.WriteLine($"Could not find member with id: {id}");
                }
                return null;
            }
            void displayMemberInformation(Member member)
            {
                string gender;
                if(member.GenderTypeID ==1) { gender = "Female"; } else { gender = "Male"; }
                Console.WriteLine();
                Console.WriteLine($"Name: {member.ParliamentaryName}");
                Console.WriteLine($"Preferred Name: {member.PreferredName}");
                if (member.BirthDate != null) { Console.WriteLine($"Birth Date: {member.BirthDate.Value.ToShortDateString()}"); }
                Console.WriteLine($"Birth Date Protected: {member.BirthDateIsProtected}");
                Console.WriteLine($"Gender: {gender}");
                Console.WriteLine($"Notes: {member.Notes}");
                Console.WriteLine($"Photo URL: {member.PhotoURL}");
            }

            Member SelectPropertyToEdit(Member member)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("              Current              ");
                Console.WriteLine("-----------------------------------");
                if (member != null) { displayMemberInformation(member); }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();

                bool stillEditing = true;
                while (stillEditing)
                {
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("             ** Enter 'exit' when finished **   ");
                    Console.WriteLine("             ** Enter 'delete' to deactivate member **   ");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine();
                    Console.Write("Which field would you like to edit: ");
                    string field;
                    if (member.IsActive == false)
                    {
                        field = "delete";
                    }
                    else
                    {
                        field = Console.ReadLine().ToLower();
                    }
                   

                    switch (field)
                    {
                        case "name":
                            Console.Write("Name: ");
                            member.ParliamentaryName = Console.ReadLine();
                            ShowEdits();
                            break;
                        case "preferred name":
                            Console.Write("Preferred Name: ");
                            member.PreferredName = Console.ReadLine();
                            ShowEdits();
                            break;
                        case "birth date":
                            Console.Write("Birth Date: ");
                            member.BirthDate = Convert.ToDateTime(Console.ReadLine());
                            ShowEdits();
                            break;
                        case "birth date protected":
                            Console.Write("Birth Date Protected: ");
                            member.BirthDateIsProtected = Convert.ToBoolean(Console.ReadLine());
                            ShowEdits();
                            break;
                        case "gender":
                            Console.Write("Select Gender (M or F): ");
                            var gender = Console.ReadLine().ToLower();
                            int genderType = 1;
                            if (gender == "m") { genderType = 2; }
                            member.GenderTypeID = genderType;
                            ShowEdits();
                            break;
                        case "notes":
                            Console.Write("Notes: ");
                            member.Notes = Console.ReadLine();
                            ShowEdits();
                            break;
                        case "photo url":
                            Console.Write("Photo URL: ");
                            member.PhotoURL = Console.ReadLine();
                            ShowEdits();
                            break;
                        case "exit":
                            stillEditing = false;
                            break;
                        case "delete":
                            member.IsActive = false;
                            stillEditing = false;
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("**Please select a valid field or type 'exit' to contitnue**");
                            break;
                    }
                }

                void ShowEdits()
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("                NEW                ");
                    Console.WriteLine("-----------------------------------");
                    if (member != null) { displayMemberInformation(member); }
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------");
                }

                return member;
            }
            void SaveMember(Member member)
            {
                Console.WriteLine();
                if(member.IsActive == true)
                {
                    Console.Write("Do you want to save?: ");
                }
                else if(member.IsActive == false)
                {
                    Console.Write("Are you sure you want to DELETE? ");
                }

                string selection = Console.ReadLine().ToLower();
                Console.WriteLine();
                
                if (Options.yesOptions.Any(y => y == selection))
                {
                    dBAccess.UpdateMember(member);
                    if (member.IsActive == true)
                    {
                        Console.Write("**SAVED**");
                    }
                    else if (member.IsActive == false)
                    {
                        Console.WriteLine("**DELETED**");
                    }
                }
                else if (Options.noOptions.Any(n => n == selection))
                {
                    GoBackToMainMenu();
                }
                else
                {
                    Console.WriteLine("** Please say Yes or No **");
                    SaveMember(member);
                }
            }

        }
    }
}
