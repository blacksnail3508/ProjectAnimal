public static class IntExtension
{
    public static int DivideRoundUp(this int dividend , int divisor)
    {
        // Tính kết quả của phép chia
        int result = dividend/divisor;

        // Nếu có phần dư, kết quả cần được làm tròn lên bằng cách cộng thêm 1
        if (dividend%divisor!=0)
        {
            result++;
        }

        return result;
    }
}
