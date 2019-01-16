using System;

namespace Assignment1
{
    class StaffMenu
    {
        private static StaffMenu uniqueInstance;

        private StaffMenu()
        {

        }

        public static StaffMenu GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new StaffMenu();
            }
            return uniqueInstance;
        }

        public void staffMenuActive()
        {
                StaffManager stfManager = new StaffManager();
                do
                {
                    Console.WriteLine("\n------------------------------------------------------------" +
                    "\nStaff menu:  1.List staff    2.Room availability    3.Create slot    4.Remove slot   5.Exit");
                    Console.WriteLine("Enter option: ");
                    var option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            stfManager.ListStaff();
                            break;

                        case 2:
                            stfManager.RoomAvaliability();
                            break;

                        case 3:
                            stfManager.CreateSlot();
                            break;

                        case 4:
                            stfManager.RemoveSlot();
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
