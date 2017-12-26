using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace _3rdTry
{
    
    public partial class _Default : Page
    {
        public class Block
        {

            public Block()
            {
                int index = -1;
                string time = "";
                string data = "";
                string currentHash = "";
                string previousHash = "";
            }

            public int Getindex() { return index; }
            public void Setindex(int val) { index = val; }
            public string Gettime() { return time; }
            public void Settime(string val) { time = val; }
            public string Getdata() { return data; }
            public void Setdata(string val) { data = val; }
            public string Gethash() { return currentHash; }
            public void Sethash(string val) { currentHash = val; }
            public string GetpreviousHash() { return previousHash; }
            public void SetpreviousHash(string val) { previousHash = val; }

            public bool ValidPrevIndex(Block previousBlock, Block currentBlock) //verifies that current block has a valid index according to previous block
            {
                if (previousBlock.Getindex() == currentBlock.Getindex() - 1)
                    return true;
                else
                    return false;
            }

            public bool ValidPrevHash(Block previousBlock, Block currentBlock) //verifies that current block has a valid hash according to previous block
            {
                if (previousBlock.Gethash() == currentBlock.GetpreviousHash())
                    return true;
                else
                    return false;
            }

            public void calculateHash() //calculate a hash value for current block
            {
                // step 1, calculate MD5 hash from input
                MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(previousHash);
                byte[] hash = md5.ComputeHash(inputBytes);
                // step 2, convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                currentHash = sb.ToString();
            }


            private int index;
            private string time;
            private string data;
            private string currentHash;
            private string previousHash;
        }
        public class BlockChain
        {
            public BlockChain()
            {
                fullChain.Add(generateOriginalBlock());
            }

            public List<Block> GetChain() { return fullChain; }

            public Block getLatestBlock() //returns the latest block in list
            {
                return fullChain[fullChain.Count - 1];
            }

            public void generateNextBlock(string newData)
            {
                Block b = new Block();
                b.Setdata(newData);
                b.Setindex(fullChain[fullChain.Count - 1].Getindex() + 1);
                b.SetpreviousHash(fullChain[fullChain.Count - 1].Gethash());
                b.calculateHash();

                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                b.Settime(milliseconds.ToString());

                fullChain.Add(b);
            }

            public string printChain()
            {
                string entireChain = "";

                for (int i = 0; i < fullChain.Count; i++)
                {
                    entireChain += "Index: " + fullChain[i].Getindex() + "<br />";
                    entireChain += "Hash: " + fullChain[i].Gethash() + "<br />";
                    entireChain += "Previous Hash: " + fullChain[i].GetpreviousHash() + "<br />";
                    entireChain += "Time: " + fullChain[i].Gettime() + "<br />";
                    entireChain += "Data: " + fullChain[i].Getdata() + "<br /><br />" + "<br />";
                }

                return entireChain;
            }

            public Block generateOriginalBlock()
            {
                Block b = new Block();
                b.Setdata("first Block");
                b.Setindex(0);
                b.SetpreviousHash("0");
                b.calculateHash();

                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                b.Settime(milliseconds.ToString());

                return b;
            }

            private List<Block> fullChain = new List<Block>();
        }

        static string blockChainStr = "";
        static BlockChain fullChain = new BlockChain();

        public string getBlockChain()
        {
            return blockChainStr;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Update(object sender, EventArgs e)
        {
            fullChain.generateNextBlock(UserName.Text.ToString());
            blockChainStr = fullChain.printChain();
        }
    }  
}