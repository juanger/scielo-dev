class Statistic

  def initialize
    @stats = File.new(".migrator-stats", "w")
    @counters = {
      :journal => 0,
      :issue => 0,
      :article => 0,
      :author => 0,
      :journal_ref => 0,
      :issue_ref => 0,
      :article_ref => 0,
      :auth_wol => 0,
      :auth_wof => 0
    }
  end

  def add entity
    @counters[entity] += 1
  end

  def close
    msgs = [
            ["Revistas", :journal],
            ["Issues", :issue],
            ["Articulos", :article],
            ["Autores", :author],
            ["Revistas de Referencia", :journal_ref],
            ["Issues de Referencia", :issue_ref],
            ["Articulos de Referencia", :article_ref],
            ["Autores de Referencia sin Apellidos:", :auth_wol],
            ["Autores de Referencia sin Nombre:", :auth_wof],
           ]

    msgs.each do |msg|
      @stats.puts "Número de #{msg[0]}: #{@counters[msg[1]]}"

    end
    @stats.close
  end

end
