using System;
using System.Text.RegularExpressions;
class getCoors
{
    /***************************** ���ʽ ***********************************/
    /// <summary>
    /// ����ϵ����AΪԭ�����������ϵ
    /// </summary>
    /// <param name="MA"></param>
    /// <param name="MB"></param>
    /// <param name="MC"></param>
    /// <param name="AB"></param>
    /// <param name="BC"></param>
    /// <param name="AC"></param>
    /// <returns></returns>
    public static Tuple<double, double, double> Cal2(double MA, double MB, double MC, double AB, double BC, double AC)
    {
        double CosMAS = GetCos(MA, MB, AB);
        double CosMAC = GetCos(MA, MC, AC);
        double CosCAB = GetCos(AB, BC, AC);
        //double CosMAS = Math.Cos(120.0 / 180 * Math.PI), CosMAC = Math.Cos(120.0 / 180 * Math.PI), CosCAB = Math.Cos(60.0 / 180 * Math.PI);

        double TanW = (CosMAC / CosMAS - CosCAB) / (Math.Sin(Math.Acos(CosCAB)));

        double AO = MA * CosMAS / (Math.Cos(Math.Atan(TanW)));

        //������MA��Y���ϵ�ͶӰ
        double X = AO * (Math.Cos(Math.Atan(TanW)));
        double Y = AO * (Math.Sin(Math.Atan(TanW)));
        double Z = Math.Sqrt(MA * MA - AO * AO);

        Tuple<double, double, double> Coord = Tuple.Create<double, double, double>(X, Y, Z);

        return Coord;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LengthA"></param>
    /// <param name="LengthO">��O���Ա߱߳�</param>
    /// <param name="LengthB"></param>
    /// <returns>���ؽ�O������ֵ</returns>
    private static double GetCos(double LengthA, double LengthO, double LengthB)
    {
        return (LengthA * LengthA + LengthB * LengthB - LengthO * LengthO) /
            (2 * LengthA * LengthB);
    }
    public static bool IsNumber(string input)
    {
        string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }


    /***************************** ���ʽ ***********************************/

    double CB_Length, CA_Length;

    double CosMAC, CosMCA, CosMBC, CosMCB;

    /// <summary>
    /// Pitch  ������  Horiz ˮƽ��
    /// </summary>
    /// <param name="Angle_A_Horiz">AM��ˮƽ��</param>
    /// <param name="Angle_A_Pitch">AM�ĸ�����</param>
    /// <param name="Angle_B_Horiz">BM��ˮƽ��</param>
    /// <param name="Angle_B_Pitch">BM�ĸ�����</param>
    /// <param name="Angle_C_Horiz">CM��ˮƽ��</param>
    /// <param name="Angle_C_Pitch">CM�ĸ�����</param>
    /// <returns>����AM,CM,BM�ľ���</returns>
    public Tuple<double, double, double> SetCoord(
        double Angle_A_Horiz,
        double Angle_A_Pitch,
        double Angle_B_Horiz,
        double Angle_B_Pitch,
        double Angle_C_Horiz,
        double Angle_C_Pitch)
    {
        CosMAC = Math.Cos((180 - Angle_A_Horiz) * Math.PI / 180) * Math.Cos(Angle_A_Pitch * Math.PI / 180);
        CosMCA = Math.Cos(Angle_C_Horiz * Math.PI / 180) * Math.Cos(Angle_C_Pitch * Math.PI / 180);
        //DΪM���߶�AC�ϵĴ��� dΪAD�ľ���
        double d = CA_Length / (1 + Math.Tan(Math.Acos(CosMAC)) / Math.Tan(Math.Acos(CosMCA)));
        double AM = Math.Abs(d / CosMAC);
        double CM = Math.Abs((CA_Length - d) / CosMCA);

        CosMCB = Math.Cos((90 - Angle_C_Horiz) * Math.PI / 180) * Math.Cos(Angle_C_Pitch * Math.PI / 180);
        double BM = Math.Sqrt(CM * CM + CB_Length * CB_Length - 2 * CM * CB_Length * CosMCB);

        Tuple<double, double, double> result = Tuple.Create<double, double, double>(AM, CM, BM);
        return result;
    }
}