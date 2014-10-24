using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminControlForm
{
    class BlockUnblockUserValidation
    {
        public bool
        ValidateBlockUnblockUser2(bool checkedBlock, ref string errMsg)
        {
            bool validInput = false;
            
            if (checkedBlock)      
                validInput = true;

            return validInput;
        }

        public bool
        ValidateBlockUnblockUser(string blockStatus, ref string errMsg)
        {
            const string BLOCK   = "BLOCK";
            const string UNBLOCK = "UNBLOCK";
            
            bool validInput = false;
            
            if (blockStatus.Equals(BLOCK) || blockStatus.Equals(UNBLOCK) ) {       
                validInput = true;
            }
            else {
                errMsg = "Please enter BLOCK or UNBLOCK into the field";
            }

            return validInput;
        }
    }
}
