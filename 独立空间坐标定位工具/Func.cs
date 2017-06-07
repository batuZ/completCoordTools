public class MyFunc
{
    public class CalCoord
    {
        double CB_Length, CA_Length;

        double CosMAC, CosMCA, CosMBC, CosMCB;
        /// <summary>
        /// Ĭ�Ϲ���һ����CB��CAΪ������Ŀռ�ֱ������ϵ��CAΪ��������
        /// </summary>
        /// <param name="CB_Length"></param>
        /// <param name="CA_Length"></param>
        public CalCoord(double CB_Length, double CA_Length)
        {
            this.CB_Length = CB_Length;
            this.CA_Length = CA_Length;
        }
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
}