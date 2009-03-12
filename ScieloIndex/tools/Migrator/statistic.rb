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
            ["Journals", :journal],
            ["Issues", :issue],
            ["Articles", :article],
            ["Authors", :author],
            ["Journal References", :journal_ref],
            ["Issue References", :issue_ref],
            ["Article References", :article_ref],
            ["Author References", :author_ref],
            ["Citations", :cite]
           ]

    msgs.each do |msg|
      @stats.puts "Number of #{msg[0]}: #{@counters[msg[1]]}"

    end
    @stats.close
  end

end
