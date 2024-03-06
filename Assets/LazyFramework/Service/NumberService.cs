public static class NumberService
{
    public static int Thousand = 1000;
    public static int Million = 1000000;
    public static int Billion = 1000000000;
    public static string Format(int inputNumber)
    {
        int cache = inputNumber;
        int million = cache/Million;
        cache = cache%Million;
        int thousand = cache/1000;
        cache = cache %Thousand;
        int hundred = cache/100;

        if ((inputNumber<10000))
        {
            return inputNumber.ToString();
        }

        if (inputNumber>=10000 && inputNumber <100000)
        {
            if(hundred==0)
            {
                return $"{thousand}K";
            }
            return $"{thousand}.{hundred}K";
        }

        if (inputNumber>=100000)
        {
            return $"{thousand}K";
        }

        if(inputNumber >=1000000)
        {
            return $"{million}M";
        }

        return "Cannot formatable";
    }
}
