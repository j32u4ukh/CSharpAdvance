using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance.Demo1
{
    public class CPU
    {
        // 下一條指令的位置
        public byte PC;

        // 當前或即將執行的指令
        public byte IR;

        // 正在使用的地址
        public byte MAR;

        // 正在使用的數據
        public byte MDR;

        // 暫存器
        public byte[] Register;

        // ALU 輸入一
        public byte x;

        // ALU 輸入二
        public byte y;

        // ALU 輸出
        public byte z;

        // 旗標位(7: stop)
        public byte flag;

        public CPU()
        {
            PC = 0;
            IR = 0;
            MAR = 0;
            MDR = 0;
            Register = new byte[8];
            x = 0;
            y = 0;
            z = 0;
            flag = 0;
        }

        public void Excute(byte[] memory)
        {
            byte b0 = memory[PC++];
            byte b1 = memory[PC++];
            byte cmd = (byte)(b0 >> 5);
            byte addr = (byte)(b0 & 0b00011111);
            byte val0, val1;

            switch (cmd)
            {
                case Demo.LOAD:
                    val0 = Load(b1, memory);
                    Store(addr, val0, memory);
                    val1 = Load(addr, memory);
                    Console.WriteLine($"LOAD | val0: {val0}, val1: {val1}");
                    break;
                case Demo.ADD:
                    val0 = Load(addr, memory);
                    val1 = Load(b1, memory);
                    Console.WriteLine($"ADD1 | val0: {val0}, val1: {val1}");
                    val0 += val1;
                    Store(addr, val0, memory);
                    val1 = Load(addr, memory);
                    Console.WriteLine($"ADD2 | val0: {val0}, val1: {val1}");
                    break;
                case Demo.STORE:
                    val0 = Load(b1, memory);
                    Store(addr, val0, memory);
                    val1 = Load(addr, memory);
                    Console.WriteLine($"STORE | val0: {val0}, val1: {val1}");
                    break;
                case Demo.STOREI:
                    val0 = b1;
                    Store(addr, b1, memory);
                    val1 = Load(addr, memory);
                    Console.WriteLine($"STOREI | val0: {val0}, val1: {val1}");
                    break;
                case Demo.JUMP:
                    Console.WriteLine($"JUMP | PC: {PC} -> addr: {addr}");
                    PC = addr;
                    break;
                case Demo.PRINT:
                    Console.WriteLine($">> {Load(addr, memory)}");
                    break;
                case Demo.EXIT:
                    flag = 7;
                    Console.WriteLine($"EXIT | flag: {flag}");
                    break;
                default:
                    break;
            }
        }

        public void Store(byte index, byte value, byte[] memory)
        {
            if (index >= Demo.LEN)
            {
                Register[(byte)(index - Demo.LEN)] = value;
            }
            else
            {
                memory[index] = value;
            }
        }

        public byte Load(byte index, byte[] memory)
        {
            if (index >= Demo.LEN)
            {
                return Register[(byte)(index - Demo.LEN)];
            }
            else
            {
                return memory[index];
            }
        }
    }

    public class Demo
    {
        // Load rd, rs
        public const byte LOAD = 0b000;

        // Add rd, rs
        public const byte ADD = 0b001;

        // Store rd, rs
        public const byte STORE = 0b010;

        // StoreI rd, imm
        public const byte STOREI = 0b011;

        // Jump addr
        public const byte JUMP = 0b100;

        // Print rs
        public const byte PRINT = 0b101;

        // Exit
        public const byte EXIT = 0b110;

        public const byte LEN = 20;

        byte[] memory = new byte[LEN];
        public static string program = "STOREI,[16],12; STOREI,[17],34; LOAD,R3,[16]; ADD,R3,[17]; STORE,[18],R3; PRINT,[18]; JUMP,[14]; EXIT";
        private byte index;
        private CPU cpu;

        public Demo(string program)
        {
            index = 0;
            cpu = new CPU();

            string[] commands = program.Split(';');
            string[] cmds;
            string cmd;
            foreach(string command in commands)
            {
                cmds = command.Trim().Split(',');
                cmd = cmds[0].ToUpper();
                switch (cmd)
                {
                    case "LOAD":
                        ParseLoad(cmds);
                        break;
                    case "ADD":
                        ParseAdd(cmds);
                        break;
                    case "STORE":
                        ParseStore(cmds);
                        break;
                    case "STOREI":
                        ParseStoreI(cmds);
                        break;
                    case "JUMP":
                        ParseJump(cmds);
                        break;
                    case "PRINT":
                        ParsePrint(cmds);
                        break;
                    case "EXIT":
                        ParseExit(cmds);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Excute()
        {
            while(cpu.flag != 7)
            {
                cpu.Excute(memory);
            }
        }

        private byte parseAddress(string cmd)
        {
            if (cmd.StartsWith("["))
            {
                return byte.Parse(cmd.Substring(1, cmd.Length - 2));
            }
            else if (cmd.StartsWith("R"))
            {
                return (byte)(LEN + byte.Parse(cmd.Substring(1, cmd.Length - 1)));
            }
            else
            {
                throw new Exception($"Parse address error, cmd: {cmd}.");
            }
        }

        /// <summary>
        /// Load rd, rs
        /// </summary>
        /// <param name="cmds"></param>
        private void ParseLoad(string[] cmds)
        {
            byte b = LOAD << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch(Exception err)
            {
                Console.WriteLine($"ParseLoad failed, err: {err}");
                return;
            }

            try
            {
                cmd = cmds[2];
                b = parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseLoad failed, err: {err}");
                return;
            }
        }

        /// <summary>
        /// Add rd, rs
        /// </summary>
        /// <param name="cmds"></param>
        private void ParseAdd(string[] cmds)
        {
            byte b = ADD << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseAdd failed, err: {err}");
                return;
            }

            try
            {
                cmd = cmds[2];
                b = parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseAdd failed, err: {err}");
                return;
            }
        }

        // Store rd, rs
        private void ParseStore(string[] cmds)
        {
            byte b = STORE << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseStore failed, err: {err}");
                return;
            }

            try
            {
                cmd = cmds[2];
                b = parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseStore failed, err: {err}");
                return;
            }
        }

        // StoreI rd, imm
        private void ParseStoreI(string[] cmds)
        {
            byte b = STOREI << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseStoreI failed, err: {err}");
                return;
            }

            try
            {
                cmd = cmds[2];
                b = byte.Parse(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseStoreI failed, err: {err}");
                return;
            }
        }

        /// <summary>
        /// Jump addr
        /// </summary>
        /// <param name="cmds"></param>
        private void ParseJump(string[] cmds)
        {
            byte b = JUMP << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParseJump failed, err: {err}");
                return;
            }

            // Empty value
            index++;
        }

        /// <summary>
        /// Print rs
        /// </summary>
        /// <param name="cmds"></param>
        private void ParsePrint(string[] cmds)
        {
            byte b = PRINT << 5;
            string cmd;

            try
            {
                cmd = cmds[1];
                b |= parseAddress(cmd);
                memory[index] = b;
                index++;
            }
            catch (Exception err)
            {
                Console.WriteLine($"ParsePrint failed, err: {err}");
                return;
            }

            // Empty value
            index++;
        }

        private void ParseExit(string[] cmds)
        {
            memory[index] = EXIT << 5;
            index++;

            // Empty value
            index++;
        }
    }
}
