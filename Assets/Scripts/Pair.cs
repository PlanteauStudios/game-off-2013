public class Pair<K,T> {
    public K first;
    public T second;
    public Pair() {
        first = default(K);
        second = default(T);
    }
    public Pair(K f, T s) {
        first = f;
        second = s;
    }
}