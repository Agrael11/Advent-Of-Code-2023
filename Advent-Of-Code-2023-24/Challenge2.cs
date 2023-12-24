using Helpers;
using Microsoft.Z3;
using System.Globalization;

//Not this is my first time using Z3. I am okay with it,
//but making my own function to get answer to system of 9 expressions with 9 unknowns would be better

namespace AdventOfCode.Day24
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Parse the input into list of hails with their position and velocity
            List<(Vector3d position, Vector3d velocity)> hails = [];

            foreach (string inputLine in inputData)
            {
                string[] split = inputLine.Replace(" ", "").Split('@');
                string[] position = split[0].Split(',');
                string[] velocity = split[1].Split(',');
                double x1 = double.Parse(position[0]);
                double y1 = double.Parse(position[1]);
                double z1 = double.Parse(position[2]);
                double x2 = double.Parse(velocity[0]);
                double y2 = double.Parse(velocity[1]);
                double z2 = double.Parse(velocity[2]);
                hails.Add(new(new(x1, y1, z1), new(x2, y2, z2)));
            }

            //Get three hails with different velocities
            int index0 = 0;
            int index1 = 0;
            while (hails[index1].velocity == hails[index0].velocity)
            {
                index1++;
            }
            int index2 = index1+1;
            while (hails[index2].velocity == hails[index0].velocity || hails[index2].velocity == hails[index1].velocity)
            {
                index2++;
            }

            Context ctx = new();

            //Unknow variables
            RealExpr t = ctx.MkRealConst("t"); //Time for first hail
            RealExpr u = ctx.MkRealConst("u"); //Time for Second hail
            RealExpr v = ctx.MkRealConst("v"); //Time for third hail
            RealExpr SPx = ctx.MkRealConst("SPx"); //Position of stone
            RealExpr SPy = ctx.MkRealConst("SPy");
            RealExpr SPz = ctx.MkRealConst("SPz");
            RealExpr SVx = ctx.MkRealConst("SVx"); //Velocity of stone
            RealExpr SVy = ctx.MkRealConst("SVy");
            RealExpr SVz = ctx.MkRealConst("SVz");

            //Known variables
            RealExpr H0Px = ctx.MkReal(hails[index0].position.X.ToString(CultureInfo.InvariantCulture)); //Position and velocity of first hail
            RealExpr H0Py = ctx.MkReal(hails[index0].position.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H0Pz = ctx.MkReal(hails[index0].position.Z.ToString(CultureInfo.InvariantCulture));
            RealExpr H0Vx = ctx.MkReal(hails[index0].velocity.X.ToString(CultureInfo.InvariantCulture));
            RealExpr H0Vy = ctx.MkReal(hails[index0].velocity.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H0Vz = ctx.MkReal(hails[index0].velocity.Z.ToString(CultureInfo.InvariantCulture));

            RealExpr H1Px = ctx.MkReal(hails[index1].position.X.ToString(CultureInfo.InvariantCulture)); //Position and velocity of second hail
            RealExpr H1Py = ctx.MkReal(hails[index1].position.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H1Pz = ctx.MkReal(hails[index1].position.Z.ToString(CultureInfo.InvariantCulture));
            RealExpr H1Vx = ctx.MkReal(hails[index1].velocity.X.ToString(CultureInfo.InvariantCulture));
            RealExpr H1Vy = ctx.MkReal(hails[index1].velocity.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H1Vz = ctx.MkReal(hails[index1].velocity.Z.ToString(CultureInfo.InvariantCulture));

            RealExpr H2Px = ctx.MkReal(hails[index2].position.X.ToString(CultureInfo.InvariantCulture)); //Position and velocity of third hail
            RealExpr H2Py = ctx.MkReal(hails[index2].position.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H2Pz = ctx.MkReal(hails[index2].position.Z.ToString(CultureInfo.InvariantCulture));
            RealExpr H2Vx = ctx.MkReal(hails[index2].velocity.X.ToString(CultureInfo.InvariantCulture));
            RealExpr H2Vy = ctx.MkReal(hails[index2].velocity.Y.ToString(CultureInfo.InvariantCulture));
            RealExpr H2Vz = ctx.MkReal(hails[index2].velocity.Z.ToString(CultureInfo.InvariantCulture));


            /*
             * Equastion system. Basically
             * Hail0.Position + Hail0.Velocity * t == Stone.Position + Stone.Velocity * t
             * Hail1.Position + Hail1.Velocity * u == Stone.Position + Stone.Velocity * u
             * Hail2.Position + Hail2.Velocity * v == Stone.Position + Stone.Velocity * v
             */
            BoolExpr[] equations =
            {
                ctx.MkEq(H0Px + H0Vx * t, SPx + SVx * t),
                ctx.MkEq(H0Py + H0Vy * t, SPy + SVy * t),
                ctx.MkEq(H0Pz + H0Vz * t, SPz + SVz * t),
                ctx.MkEq(H1Px + H1Vx * u, SPx + SVx * u),
                ctx.MkEq(H1Py + H1Vy * u, SPy + SVy * u),
                ctx.MkEq(H1Pz + H1Vz * u, SPz + SVz * u),
                ctx.MkEq(H2Px + H2Vx * v, SPx + SVx * v),
                ctx.MkEq(H2Py + H2Vy * v, SPy + SVy * v),
                ctx.MkEq(H2Pz + H2Vz * v, SPz + SVz * v),
            };

            //Add each equation as verification check into solver
            Solver solver = ctx.MkSolver();
            foreach (var eq in equations)
            {
                solver.Assert(eq);
            }

            //Checks whether solution is oslvable
            Status status = solver.Check();

            //If it is
            if (status == Status.SATISFIABLE)
            {
                //Get solution
                Model model = solver.Model;

                //And it's specific variables
                RatNum x = (RatNum)model.Evaluate(SPx);
                RatNum y = (RatNum)model.Evaluate(SPy);
                RatNum z = (RatNum)model.Evaluate(SPz);
                long realX = x.Numerator.Int64;
                long realY = y.Numerator.Int64;
                long realZ = z.Numerator.Int64;

                return realX+realY+realZ;
            }
            
            throw new Exception("No solution");
        }
    }
} 