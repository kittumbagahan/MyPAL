using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TeacherDeviceController{

	
   public bool SetAsTeacherDevice()
   {
      DataService ds = new DataService ();
      TeacherDeviceModel model = ds._connection.Table<TeacherDeviceModel> ().Where(x=>x.Id == 1).FirstOrDefault();
      if(model == null)
      {
         TeacherDeviceModel m = new TeacherDeviceModel {
            DeviceId = SystemInfo.deviceUniqueIdentifier
         };
         ds._connection.Insert (m);
         return true;
      }
      Debug.Log (model.ToString());
      return false;
   }

   public bool IsTeacherDevice()
   {
      DataService ds = new DataService ();
      var model = ds._connection.Table<TeacherDeviceModel> ().Where (x => x.DeviceId == SystemInfo.deviceUniqueIdentifier).FirstOrDefault ();
      if (model != null)
      {
         return true;
      }
      return false;
   }
}
