﻿namespace PterodactylCharts
{
    public class GraphElements
    {
        public GraphElements(GraphData graphData, GraphLegend graphLegend)
        {
            Data = graphData;
            Legend = graphLegend;
        }
        public override string ToString()
        {
            return "Graph Elements";
        }
        public GraphData Data { get; set; }
        public GraphLegend Legend { get; set; }
    }
}
