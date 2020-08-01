using System.Collections.Generic;
namespace isn{

    public interface IUIBullet
    {
        void OnBulletRegister(int regID);
        void OnBulletRelease();
    }
    public class BulletMgr: Singleton<BulletMgr>{
        Dictionary<int, IUIBullet> registeredBullets;
        public void Register(IUIBullet bullet){
            int id = registeredBullets.Count;
            while(registeredBullets.ContainsKey(id))
            {
                id++;
            }
            registeredBullets.Add(id, bullet);
            bullet.OnBulletRegister(id);
        }
    }
}