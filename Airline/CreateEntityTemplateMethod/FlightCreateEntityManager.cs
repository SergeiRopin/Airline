using Airline.TemplateMethod;
using Airport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class FlightCreateEntityManager : CreateEntityManager<IEntity>
    {
        protected override bool AskQuestion()
        {
            Console.Write("\nDo you want to create a new flight? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswer()
        {
            var answer = new StringBuilder();

            var arrivalDeparture = CreateArrivalDeparture();
            answer.Append(AddSeparator(arrivalDeparture.ToString()));

            string number = CreateFlightNumber();
            answer.Append(AddSeparator(number));

            string cityFrom = CreateDepartureCity();
            answer.Append(AddSeparator(cityFrom));

            string cityTo = CreateArrivalCity();
            answer.Append(AddSeparator(cityTo));

            string airline = CreateAirline();
            answer.Append(AddSeparator(airline));

            var terminal = CreateTerminal();
            answer.Append(AddSeparator(terminal.ToString()));

            var gate = CreateGate();
            answer.Append(AddSeparator(gate.ToString()));

            var status = CreateFlightStatus();
            answer.Append(AddSeparator(status.ToString()));

            DateTime flightTime = CreateFlightTime();
            answer.Append(flightTime.ToString());

            return answer.ToString();
        }

        protected override bool IsValid(string value)
        {
            var parameters = value.Split('|');
            return parameters.Count() == 9;
        }

        protected override IEntity CreateEntity(string value)
        {
            var parameters = value.Split('|');
            return new Flight((ArrivalDeparture)Enum.Parse(typeof(ArrivalDeparture), parameters[0]), parameters[1], parameters[2], parameters[3],
               parameters[4], (Terminal)Enum.Parse(typeof(Terminal), parameters[5]), (Gate)Enum.Parse(typeof(Gate), parameters[6]),
               (FlightStatus)Enum.Parse(typeof(FlightStatus), parameters[7]), Convert.ToDateTime(parameters[8]), new List<Passenger>());
        }


        protected override bool AskEditQuestion()
        {
            Console.Write("\nDo you want to edit the flight? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadEditAnswer(IEntity actualFlight)
        {
            var editEntityHelper = new EditEntityHelper();
            var answer = new StringBuilder();

            //Edit arrival/departure info.
            InputOutputHelper.PrintColorText($"\nActual flight type: {actualFlight.GetType().GetProperty("ArrivalDeparture").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<ArrivalDeparture> arrivalDepartureHandler = CreateArrivalDeparture;
            var arrivalDeparture = editEntityHelper.EditEntity(arrivalDepartureHandler);
            answer.Append(AddSeparator(arrivalDeparture.ToString()));

            //Edit flight number.
            InputOutputHelper.PrintColorText($"\nActual flight number: {actualFlight.GetType().GetProperty("Number").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<string> flightNumberHandler = CreateFlightNumber;
            string number = editEntityHelper.EditEntity(flightNumberHandler);
            answer.Append(AddSeparator(number));

            //Edit city of departure.
            InputOutputHelper.PrintColorText($"\nActual departure city: {actualFlight.GetType().GetProperty("CityFrom").GetValue(actualFlight)}", ConsoleColor.DarkCyan);
            Func<string> cityFromHandler = CreateDepartureCity;
            string cityFrom = editEntityHelper.EditEntity(cityFromHandler);
            answer.Append(AddSeparator(cityFrom));

            //Edit city of arrival.
            InputOutputHelper.PrintColorText($"\nActual arrival city: {actualFlight.GetType().GetProperty("CityTo").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<string> cityToHandler = CreateArrivalCity;
            string cityTo = editEntityHelper.EditEntity(cityToHandler);
            answer.Append(AddSeparator(cityTo));

            //Edit airline.
            InputOutputHelper.PrintColorText($"\nActual airline: {actualFlight.GetType().GetProperty("Airline").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<string> airlineHandler = CreateAirline;
            string airline = editEntityHelper.EditEntity(airlineHandler);
            answer.Append(AddSeparator(airline));

            //Edit terminal.
            InputOutputHelper.PrintColorText($"\nActual terminal: {actualFlight.GetType().GetProperty("Terminal").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<Terminal> terminalHandler = CreateTerminal;
            var terminal = editEntityHelper.EditEntity(terminalHandler);
            answer.Append(AddSeparator(terminal.ToString()));

            //Edit gate.
            InputOutputHelper.PrintColorText($"\nActual gate: {actualFlight.GetType().GetProperty("Gate").GetValue(actualFlight)}", ConsoleColor.DarkCyan);
            Func<Gate> gateHandler = CreateGate;
            var gate = editEntityHelper.EditEntity(gateHandler);
            answer.Append(AddSeparator(gate.ToString()));

            //Edit flight status.
            InputOutputHelper.PrintColorText($"\nActual flight status: {actualFlight.GetType().GetProperty("Status").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<FlightStatus> statusHandler = CreateFlightStatus;
            var status = editEntityHelper.EditEntity(statusHandler);
            answer.Append(AddSeparator(status.ToString()));

            //Edit fligth time.
            InputOutputHelper.PrintColorText($"\nActual flight date and time: {actualFlight.GetType().GetProperty("DateTime").GetValue(actualFlight)}",
                ConsoleColor.DarkCyan);
            Func<DateTime> timeHandler = CreateFlightTime;
            DateTime flightTime = editEntityHelper.EditEntity(timeHandler);
            answer.Append(flightTime.ToString());

            return answer.ToString();
        }

        protected override IEntity EditEntity(string value, IEntity actualFlight)
        {
            var parameters = value.Split('|');
            for (int i = 0; i < parameters.Length; i++)
            {
                if(parameters[i] == null)
                {

                }
            }
            return new Flight((ArrivalDeparture)Enum.Parse(typeof(ArrivalDeparture), parameters[0]), parameters[1], parameters[2], parameters[3],
                parameters[4], (Terminal)Enum.Parse(typeof(Terminal), parameters[5]), (Gate)Enum.Parse(typeof(Gate), parameters[6]),
                (FlightStatus)Enum.Parse(typeof(FlightStatus), parameters[7]), Convert.ToDateTime(parameters[8]), new List<Passenger>());
        }

        #region CreateFlightMethods

        private ArrivalDeparture CreateArrivalDeparture()
        {
            var arrivalDeparture = InputOutputHelper.CheckEnumInput<ArrivalDeparture>
                ("\n" + @"Enter a flight type. Choose a number from the following list:
                1. Arrival
                2. Departure");
            return arrivalDeparture;
        }

        private string CreateFlightNumber()
        {
            string number = InputOutputHelper.CheckStringInput("\nEnter a number of the flight: ");
            return number;
        }

        private string CreateDepartureCity()
        {
            string cityFrom = InputOutputHelper.CheckStringInput("\nEnter a city of departure: ");
            return cityFrom;
        }

        private string CreateArrivalCity()
        {
            string cityTo = InputOutputHelper.CheckStringInput("\nEnter a city of arrival: ");
            return cityTo;
        }

        private string CreateAirline()
        {
            string airline = InputOutputHelper.CheckStringInput("\nEnter an airline: ");
            return airline;
        }

        private Terminal CreateTerminal()
        {
            var terminal = InputOutputHelper.CheckEnumInput<Terminal>("\n" + @"Enter a terminal of the flight. Choose a number from the following list:
                1. A
                2. B");
            return terminal;
        }

        private Gate CreateGate()
        {
            var gate = InputOutputHelper.CheckEnumInput<Gate>("\n" + @"Enter a gate of the flight. Choose a number from the following list:
                1. A1
                2. A2
                3. A3
                4. A4");
            return gate;
        }

        private FlightStatus CreateFlightStatus()
        {
            var status = InputOutputHelper.CheckEnumInput<FlightStatus>("\n" + @"Enter a flight status. Choose a number from the following list:
                1. CheckIn
                2. GateClosed
                3. Arrived
                4. DeparturedAt
                5. Unknown
                6. Canceled
                7. ExpectedAt
                8. Delayed
                9. InFlight
                10. Boarding");
            return status;
        }

        private DateTime CreateFlightTime()
        {
            DateTime flightTime = InputOutputHelper.CheckDateTimeInput("\nEnter a flight time in the following format: ");
            return flightTime;
        }
        #endregion
    }
}
