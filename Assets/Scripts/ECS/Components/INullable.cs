public interface INullable<T> where T : struct
{
    void Nullable(ref T component);
}
