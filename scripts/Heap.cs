using System;
namespace isn{
    public class MinHeap<T> where T:IComparable<T>{

        T[] e;
        public int Count{get; private set; }
        public MinHeap(int capacity){
            int alloc = 1;
            while(alloc<capacity){
                alloc<<=1;
            }
            e = new T[alloc];
            Count = 0;
        }

        public T Top(){
            return e[1];
        }

        public T Pop(){
            if(Count<1)
                return default(T);
            var result = Top();
            e[1] = e[Count];
            e[Count] = default(T);
            Count--;
            Adjust();
            Console.WriteLine("Heap pop "+result);
            return result;
        }

        public void Adjust(){
            int cur = 1;
            while(cur>=0){
                cur = Adjust(cur);
            }
        }

        private int Adjust(int i){
            int min = i;
            int l = i<<1;
            int r = l+1;
            if(e[i] == null || e[l] == null){
                return -1;
            }

            if(e[min].CompareTo(e[l])> 0){
                min = l;
            }

            if(e[r]!=null && e[min].CompareTo(e[r])>0){
                min = r;
            }

            if(min!=i){
                var temp = e[i];
                e[i] = e[min];
                e[min] = temp;
                return min;
            }
            return -1;
        }

        public void Add(T element){
            int last = Count+1;
            e[last]=element;
            while(last>1){
                int next = last>>1;
                if(e[next].CompareTo(e[last])>0){
                    var temp = e[next];
                    e[next] = e[last];
                    e[last] = temp;
                    last = next;
                }
                else{
                    break;
                }
            }            
            Count++;
            Console.WriteLine("\nTimer stack size: "+Count);
            for(int i=1;i<=Count;i++){
                Console.Write(e[i]);
                Console.Write(" ");
            }
        }
    }
}