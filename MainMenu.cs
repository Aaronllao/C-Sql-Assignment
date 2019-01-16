using System;

namespace Assignment1
{
    class MainMenu
    {
        private static MainMenu uniqueInstance;
        private StaffMenu stamenu;
        private StudentMenu stumenu;

        private MainMenu()
        {
            stamenu = StaffMenu.GetInstance();
            stumenu = StudentMenu.GetInstance();

        }

        public static MainMenu GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new MainMenu();
            }
            return uniqueInstance;
        }



        public void avtive()
        {
           //var staff = new StaffMenu();
            //var student = new StudentMenu();
            var mmm = new MainMenuManager();
            Console.WriteLine("------------------------------------------------------------" +
                "\nWelcome to Appointment Scheduling and Reservation System" +
                "\n------------------------------------------------------------" +
                "\n"); 

                do
                {
                    Console.WriteLine("\n------------------------------------------------------------" +
                        "\nMain menu:   1.List rooms    2.List slots    3.Staff menu    4.Student menu  5.Exit");
                    Console.WriteLine("Enter option: ");
                    var option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            mmm.ListRoom();
                            break;

                        case 2:
                            mmm.ListSlot();
                            break;

                        case 3:
                            stamenu.staffMenuActive();
                            break;

                        case 4:
                            stumenu.studentMenuActive();
                            break;
                    
                        case 5:
                            Console.WriteLine("Terminating ASR.");
                            return;

                        default:
                            Console.WriteLine("Invalid input, please enter again");
                            break;
                    }
                } while (true);
        }
    }
}
