Image<Bgr, Byte> imgInput = new Image<Bgr, Byte>(new Bitmap(pictureBox1.Image));
            Image<Gray, Byte> gray = imgInput.Convert<Gray, Byte>();
         /*   Gray cannyThreshold = new Gray(100);
            Gray cannyThresholdLinking = new Gray(60);
            Gray circleAccumulatorThreshold = new Gray(60);  */
            Image<Gray, Byte> cannyEdges = gray.Canny(100, 60);
            LineSegment2D[] lines = cannyEdges.HoughLinesBinary(1, Math.PI / 90.0, 100, 200, 20)[0];
            Image<Bgr, Byte> lineImage = imgInput.CopyBlank();
            Image<Gray, Byte> Findcontour = new Image<Gray, byte>(imgInput.Width, imgInput.Height);
            Image<Bgr, byte> Drawcontour = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);
            VectorOfVectorOfPoint contour = new VectorOfVectorOfPoint();
            foreach (var line in lines)
            {
                PointF vector = line.Direction;
                double angle = Math.Atan2(vector.Y, vector.X) * 180.0 / Math.PI;
                if (angle >90 && angle<110)
                {
                    CvInvoke.FindContours(cannyEdges, contour, Findcontour, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);
                    for (int i = 0; i < contour.Size; i++)
                         CvInvoke.DrawContours(Drawcontour, contour, i, new MCvScalar(255, 0, 255, 255), 2);
                    pictureBox1.Image = Drawcontour.ToBitmap();
                }
                lineImage.Draw(line, new Bgr(Color.Aquamarine), 1);
            }
                pictureBox1.Image = lineImage.ToBitmap();
        }
