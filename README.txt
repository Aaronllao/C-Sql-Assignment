The console application uses facade pattern. Because there are one main menu and 2 sub menus (staff menu and student menu), program.cs is 
like the client. When the user is using the application, they tell the mainmenu what they want to do and it will be delivered to sub menu, so 
don't need to operate the very single method, the specific method is called by the sub menu which make the application easier to use.

I also use Singleton pattern, as this assignment is a menu and apperantly the menu must be only one. So I use Singleton pattern in the main
menu and 2 sub menus.