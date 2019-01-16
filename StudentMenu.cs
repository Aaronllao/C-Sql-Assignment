using System;

namespace Assignment1
{
    class StudentMenu
    {
        private static StudentMenu uniqueInstance;

        private StudentMenu()
        {

        }

        public static StudentMenu GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new StudentMenu();
            }
            return uniqueInstance;
        }


        public void studentMenuActive()
        {
            StudentManager stuManager = new StudentManager();
                do
                {
                    Console.WriteLine("\n------------------------------------------------------------" +
                    "\nStudent menu:  1.List students    2.Staff availability    3.Make booking    4.Delete booking   5.Exit");
                    Console.WriteLine("Enter option: ");
                    var option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            stuManager.ListStudent();
                            break;

                        case 2:
                            stuManager.StaffAvaliability();
                            break;

                        case 3:
                            stuManager.MakeBooking();
                            break;

                        case 4:
                            stuManager.CancelBooking();
                            break;

                        case 5:
                            return;

                        default:
                            Console.WriteLine("Invalid input, please enter again");
                            break;
                    }
                } while (true);
        }
    }
}
