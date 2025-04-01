using System;
using Ladeskab.lib.Interfaces;
using Ladeskab.lib;
using UsbSimulator;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the system components
        IDoor door = new Door();
        IRfidReader rfidReader = new RfidReader();
        ILog log = new FileLogger("LadeskabLog.txt");
        IUsbCharger usbCharger = new UsbChargerSimulator();
        IDisplay display = new Display();
        IChargeControl chargeControl = new ChargeControl(usbCharger, display);
        StationControl stationControl = new StationControl(chargeControl, door, rfidReader, display, log);

        // Dictionary to map input to corresponding actions
        var actions = new Dictionary<char, Action>
        {
            { 'E', () => ExitProgram() },
            { 'O', () => door.OnDoorOpen() },
            { 'C', () => door.OnDoorClose() },
            { 'R', () => HandleRfidInput(rfidReader) }
        };

        // Main loop for user interaction
        bool finish = false;
        while (!finish)
        {
            string input = GetUserInput();
            input = input.ToUpper();
            if (string.IsNullOrEmpty(input)) continue;

            // Check if the action exists in the dictionary
            if (actions.ContainsKey(input[0]))
            {
                actions[input[0]](); // Execute corresponding action
            }
            else
            {
                System.Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }

    // Method to handle the exit action
    private static void ExitProgram()
    {
        Console.WriteLine("Exiting program...");
        Environment.Exit(0); // Exit the program
    }

    // Method to handle RFID input
    private static void HandleRfidInput(IRfidReader rfidReader)
    {
        System.Console.WriteLine("Enter RFID ID: ");
        string idString = System.Console.ReadLine();

        if (int.TryParse(idString, out int id))
        {
            rfidReader.OnRfidRead(id);
        }
        else
        {
            Console.WriteLine("Invalid RFID ID. Please try again.");
        }
    }

    // Method to handle user input
    private static string GetUserInput()
    {
        System.Console.WriteLine("Enter one of the following:");
        System.Console.WriteLine("'E' for Exit");
        System.Console.WriteLine("'O' for Open");
        System.Console.WriteLine("'C' for Close");
        System.Console.WriteLine("'R' for RFID");
        System.Console.WriteLine("");
        return Console.ReadLine();
    }
}
