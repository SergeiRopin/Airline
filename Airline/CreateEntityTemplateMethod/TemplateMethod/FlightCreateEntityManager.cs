using Airline.SetDateStrategy;
using Airline.TemplateMethod;
using AirportManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    class FlightCreateEntityManager : CreateEntityManager<IEntity>
    {
        protected override bool AskQuestionCreateEntity()
        {
            Console.Write("\nDo you want to create a new flight? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswerCreateEntity()
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
            return new Flight(
                (ArrivalDeparture)Enum.Parse(typeof(ArrivalDeparture), parameters[0]),
                parameters[1],
                parameters[2],
                parameters[3],               
                parameters[4], 
                (Terminal)Enum.Parse(typeof(Terminal), parameters[5]),
                (Gate)Enum.Parse(typeof(Gate), parameters[6]),               
                (FlightStatus)Enum.Parse(typeof(FlightStatus), parameters[7]),                
                Convert.ToDateTime(parameters[8]), 
                new List<Passenger>());
        }

        protected override bool AskQuestionEditEntity()
        {
            Console.Write("\nDo you want to edit the flight? (Y/N): ");
            var answer = Console.ReadLine();
            return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase);
        }

        protected override string ReadAnswerEditEntity(IEntity actualFlight)
        {
            var entityHelper = new CreateEditEntityHelper();
            var answer = new StringBuilder();

            //Edit arrival/departure info.
            var actualArrivalDeparture = actualFlight.GetType().GetProperty("ArrivalDeparture").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual flight type: {actualArrivalDeparture}", ConsoleColor.DarkCyan);
            Func<ArrivalDeparture> arrivalDepartureHandler = CreateArrivalDeparture;
            var arrivalDeparture = entityHelper.AskEditEntity(arrivalDepartureHandler);
            arrivalDeparture = entityHelper.UpdateEntityOrKeepDefault((ArrivalDeparture)actualArrivalDeparture, arrivalDeparture);
            answer.Append(AddSeparator(arrivalDeparture.ToString()));

            //Edit flight number.
            var actualNumber = actualFlight.GetType().GetProperty("Number").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual flight number: {actualNumber}", ConsoleColor.DarkCyan);
            Func<string> flightNumberHandler = CreateFlightNumber;
            string number = entityHelper.AskEditEntity(flightNumberHandler);
            number = entityHelper.UpdateEntityOrKeepDefault((string)actualNumber, number);
            answer.Append(AddSeparator(number));

            //Edit city of departure.
            var actualCityFrom = actualFlight.GetType().GetProperty("CityFrom").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual departure city: {actualCityFrom}", ConsoleColor.DarkCyan);
            Func<string> cityFromHandler = CreateDepartureCity;
            string cityFrom = entityHelper.AskEditEntity(cityFromHandler);
            cityFrom = entityHelper.UpdateEntityOrKeepDefault((string)actualCityFrom, cityFrom);
            answer.Append(AddSeparator(cityFrom));

            //Edit city of arrival.
            var actualCityTo = actualFlight.GetType().GetProperty("CityTo").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual arrival city: {actualCityTo}", ConsoleColor.DarkCyan);
            Func<string> cityToHandler = CreateArrivalCity;
            string cityTo = entityHelper.AskEditEntity(cityToHandler);
            cityTo = entityHelper.UpdateEntityOrKeepDefault((string)actualCityTo, cityTo);
            answer.Append(AddSeparator(cityTo));

            //Edit airline.
            var actualAirline = actualFlight.GetType().GetProperty("Airline").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual airline: {actualAirline}", ConsoleColor.DarkCyan);
            Func<string> airlineHandler = CreateAirline;
            string airline = entityHelper.AskEditEntity(airlineHandler);
            airline = entityHelper.UpdateEntityOrKeepDefault((string)actualAirline, airline);
            answer.Append(AddSeparator(airline));

            //Edit terminal.
            var actualTerminal = actualFlight.GetType().GetProperty("Terminal").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual terminal: {actualTerminal}", ConsoleColor.DarkCyan);
            Func<Terminal> terminalHandler = CreateTerminal;
            var terminal = entityHelper.AskEditEntity(terminalHandler);
            terminal = entityHelper.UpdateEntityOrKeepDefault((Terminal)actualTerminal, terminal);
            answer.Append(AddSeparator(terminal.ToString()));

            //Edit gate.
            var actualGate = actualFlight.GetType().GetProperty("Gate").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual gate: {actualGate}", ConsoleColor.DarkCyan);
            Func<Gate> gateHandler = CreateGate;
            var gate = entityHelper.AskEditEntity(gateHandler);
            gate = entityHelper.UpdateEntityOrKeepDefault((Gate)actualGate, gate);
            answer.Append(AddSeparator(gate.ToString()));

            //Edit flight status.
            var actualStatus = actualFlight.GetType().GetProperty("Status").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual flight status: {actualStatus}", ConsoleColor.DarkCyan);
            Func<FlightStatus> statusHandler = CreateFlightStatus;
            var status = entityHelper.AskEditEntity(statusHandler);
            status = entityHelper.UpdateEntityOrKeepDefault((FlightStatus)actualStatus, status);
            answer.Append(AddSeparator(status.ToString()));

            //Edit fligth time.
            var actualFlightTime = actualFlight.GetType().GetProperty("DateTime").GetValue(actualFlight);
            InputOutputHelper.PrintColorText($"\nActual flight date and time: {actualFlightTime}", ConsoleColor.DarkCyan);
            Func<DateTime> timeHandler = CreateFlightTime;
            DateTime flightTime = entityHelper.AskEditEntity(timeHandler);
            flightTime = entityHelper.UpdateEntityOrKeepDefault((DateTime)actualFlightTime, flightTime);
            answer.Append(flightTime.ToString());

            return answer.ToString();
        }

        protected override IEntity EditEntity(string value, IEntity actualFlight)
        {
            var parameters = value.Split('|');
            return new Flight(
                (ArrivalDeparture)Enum.Parse(typeof(ArrivalDeparture), parameters[0]),
                parameters[1],
                parameters[2],
                parameters[3],
                parameters[4],
                (Terminal)Enum.Parse(typeof(Terminal), parameters[5]),
                (Gate)Enum.Parse(typeof(Gate), parameters[6]),
                (FlightStatus)Enum.Parse(typeof(FlightStatus), parameters[7]),
                Convert.ToDateTime(parameters[8]), 
                (List<Passenger>)actualFlight.GetType().GetProperty("Passengers").GetValue(actualFlight));
        }

        #region CreateFlightMethods

        private ArrivalDeparture CreateArrivalDeparture()
        {
            var arrivalDeparture = InputOutputHelper.SetEnum<ArrivalDeparture>
                ("\n" + @"Enter a flight type. Choose a number from the following list:
                1. Arrival
                2. Departure");
            return arrivalDeparture;
        }

        private string CreateFlightNumber()
        {
            string number = InputOutputHelper.SetString("\nEnter a number of the flight: ");
            return number;
        }

        private string CreateDepartureCity()
        {
            string cityFrom = InputOutputHelper.SetString("\nEnter a city of departure: ");
            return cityFrom;
        }

        private string CreateArrivalCity()
        {
            string cityTo = InputOutputHelper.SetString("\nEnter a city of arrival: ");
            return cityTo;
        }

        private string CreateAirline()
        {
            string airline = InputOutputHelper.SetString("\nEnter an airline: ");
            return airline;
        }

        private Terminal CreateTerminal()
        {
            var terminal = InputOutputHelper.SetEnum<Terminal>("\n" + @"Enter a terminal of the flight. Choose a number from the following list:
                1. A
                2. B");
            return terminal;
        }

        private Gate CreateGate()
        {
            var gate = InputOutputHelper.SetEnum<Gate>("\n" + @"Enter a gate of the flight. Choose a number from the following list:
                1. A1
                2. A2
                3. A3
                4. A4");
            return gate;
        }

        private FlightStatus CreateFlightStatus()
        {
            var status = InputOutputHelper.SetEnum<FlightStatus>("\n" + @"Enter a flight status. Choose a number from the following list:
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
            SetDateHelper dateHelper = new SetDateHelper(new FlightDate());
            DateTime flightTime = dateHelper.CreateDate();
            return flightTime;
        }
        #endregion
    }
}
