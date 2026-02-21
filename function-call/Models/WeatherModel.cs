namespace function_call.Models;

public record WeatherModel(Current Current);

public record Current(decimal Temp_c,decimal Temp_f,Condition Condition, int Humidity);

public record Condition(string Text);