﻿using TH.Models;

namespace TH.Classes
{
    public static class PropertyFunctions
    {
        public static bool IsValid(this Property property)
        {
            if(property.title == null)
            {
                return false;
            }

            if (property.address == null)
            {
                return false;
            }
            if (property.description == null)
            {
                return false;
            }
            if (property.created_at == null)
            {
                return false;
            }
            if (property.updated_at == null)
            {
                return false;
            }
            if (property.status == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsDisabled(this Property property)
        {
            if (property.status == "Active")
            {
                return false;
            }

            return true;
        }
    }
}
