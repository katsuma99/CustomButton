using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class MathUtils
    {
        const int LINEPOINTNUM = 5;
        public static float ChangeMapMagnif(UsbData.TouchEvent t, PointF headPos, ref int preSign, ref List<PointF> linePosList)
        {
            if (t == UsbData.TouchEvent.Down)
            {
                linePosList.Clear();
                preSign = 0;
            }

            //停止状態は点取らない
            if (linePosList.Count != 0 && Distance(headPos, linePosList.Last()) < 0.03f)
                return 0;

            linePosList.Add(headPos);
            if (linePosList.Count <= LINEPOINTNUM)
                return 0;

            linePosList.RemoveAt(0);

            double deltaAng = MathUtils.CalcAngle(linePosList[LINEPOINTNUM / 2], linePosList[0], linePosList[LINEPOINTNUM - 1]);
            int sign = (deltaAng < 0 ? -1 : 1);
            int psign = preSign;

            preSign = sign;

            if (psign == 0)
            {
                return 0;
            }
            else if (psign != sign)
            {
                //回転を安定させるため
                sign = psign;
            }

            linePosList.RemoveAt(0);
            linePosList.RemoveAt(0);

            double pi = Math.PI;
            pi *= deltaAng > 0 ? 1 : -1;
            float power = (float)(pi - deltaAng);//鋭角なほど強く180~0(pi~0) -> 0~1
            return power;
        }

        public static float Distance(PointF pos1, PointF pos2)
        {
            PointF moveVal = new PointF(pos2.X - pos1.X, pos2.Y - pos1.Y);
            return (float)Math.Sqrt(Math.Pow(moveVal.X, 2) + Math.Pow(moveVal.Y, 2));
        }

        public static double CalcAngle(PointF p1, PointF p2, PointF p3)  // p2 --- p1 --- p3   :  p1の角度
        {
            PointF v1 = new PointF(p2.X - p1.X, p2.Y - p1.Y);
            PointF v2 = new PointF(p3.X - p1.X, p3.Y - p1.Y);

            float cross = 0;
            float dot = 0;

            dot = v1.X * v2.X + v1.Y * v2.Y;
            cross = v1.X * v2.Y - v1.Y * v2.X;

            return Math.Atan2(cross, dot);
        }

        //ある点( xp, yp )がpolygの中にあるかどうかを調べる。
        //中にあれば１、外にあれば０を返す。
        public static bool CheckInOut(PointF point,PointF[] polyg)
        {
            int p;
            double sita = 0;

            for (p = 0; p < polyg.Count(); p++)
            {
                if (p != polyg.Count() - 1)
                {
                    sita += CalcAngle(point, polyg[p], polyg[p + 1]);
                }
                else
                {
                    sita += CalcAngle(point, polyg[p], polyg[0]);
                }
            }                     
            return (Math.Abs(sita) >= Math.PI);
        }

        public static PointF Normalization(PointF p)
        {
            PointF np = p;
            float x = Math.Abs(p.X);
            float y = Math.Abs(p.Y);
            if (x > y)
            {
                np.X = 1;
                np.Y = y * (1.0f / x);
            }
            else
            {
                np.X = x * (1.0f / y);
                np.Y = 1;
            }

            if (p.X < 0)
            {
                np.X *= -1;
            }
            if (p.Y < 0)
            {
                np.Y *= -1;
            }
            //Console.Write(np + "\n"); //debug<OUT>                      
            return np;
        }
    }
}
