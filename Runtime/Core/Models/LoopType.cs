namespace EasyToolkit.Fluxion
{
    public enum LoopType
    {
        /// <summary>
        /// 每次循环都从起点重新开始（从A到B，再从A到B，...）
        /// </summary>
        Restart,

        /// <summary>
        /// 每次循环都会反转方向（A到B，再B到A，再A到B...）
        /// </summary>
        Yoyo
    }
}
