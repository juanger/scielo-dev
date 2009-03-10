module Ezgraphix
  class Graphic
    # Redefinition of the class Graphic
    def to_xml
      options = parse_options(self.render_options)
      g_xml = Builder::XmlMarkup.new
      escaped_xml = g_xml.graph(options) do
        self.data.each{ |k,v|
          g_xml.set :value => v, :name => k, :color => self.rand_color 
        }
      end
      escaped_xml
      # replace double-quotes with single-quotes
      return escaped_xml.gsub("\"", "'")
    end
  end
end

module EzgraphixHelper
  def render_ezgraphix(g)
    style = get_style(g)
    xml_data = g.to_xml
    h = Hpricot("<div id='#{g.div_name}'></div>\n <script language='javascript'> var ezChart = new FusionCharts('#{f_type(g.c_type)}', '#{g.div_name}', '#{g.w}', '#{g.h}','0','0'); ezChart.setDataXML(\"#{g.to_xml}\"); ezChart.render('#{g.div_name}');</script>")
    h.to_html
  end
end