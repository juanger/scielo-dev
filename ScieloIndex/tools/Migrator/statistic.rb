class Statistic

  def initialize(stats_file="migrator-stats")
    @stats = File.new(stats_file, "w")
    @counters = {
      :journal => 0,
      :issue => 0,
      :article => 0,
      :author => 0,
      :journal_ref => 0,
      :issue_ref => 0,
      :article_ref => 0,
      :author_ref => 0,
      :cite => 0
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
            ["Autores de Referencia", :author_ref],
            ["Citas", :cite]
           ]

    msgs.each do |msg|
      @stats.puts "Número de #{msg[0]}: #{@counters[msg[1]]}"

    end
    @stats.close
  end

end
