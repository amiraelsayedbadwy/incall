using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace incalltask.Droid.utils
{
    public class ContactManager : Java.Lang.Object
    {
        public const int MAX_LINES = 10;
        private static ContactManager mInstance;
        private static System.Object locker = new System.Object();
        List<Contact> contacts = new List<Contact>();

        public static ContactManager Instatnce
        {
            get
            {
                if (mInstance == null)
                {
                    lock (locker)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new ContactManager();
                        }
                    }
                }

                return mInstance;
            }
        }
        private ContactManager()
        {
        }
        public List<Contact> getContacts()
        {
            return contacts;
        }
        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }

        public void RemoveContact(Contact contact)
        {
            contacts.Remove(contact);
        }
        public void RemoveContactAt(int index)
        {
            contacts.RemoveAt(index);
        }
        public void RemoveAll()
        {
            contacts.Clear();
        }

        public Contact FindContactBySipAddr(string sipAddr)
        {
            if (String.IsNullOrWhiteSpace(sipAddr))
                return null;
            foreach (Contact contact in contacts)
            {
                sipAddr = sipAddr.TrimStart("sip:".ToCharArray());
                String contactAddr = contact.SipAddr.TrimStart("sip:".ToCharArray());
                if (sipAddr.Equals(contactAddr))
                    return contact;
            }
            return null;
        }
    }
}