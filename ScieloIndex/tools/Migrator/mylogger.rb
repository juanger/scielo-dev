class MyLogger

  def initialize(level)

   @levels = {
    "debug" => 0,
    "warning" => 1,
    "info" => 2,
    "none" => 3
   }

    @level = @levels[level]
    if @level < 3
      @log = File.new(".migrator-log", "w")
    end
    @errors = File.new(".migrator-errors", "w")
  end

  def log(type, message)
    if @log
      @log.puts "[#{type}]: #{message}"
    end
  end

  def debug(message)
    if @level == 0
      log("Debug", message)
    end
  end

  def warning(message)
    if @level <= 1
      log("Warning", message)
    end
  end

  def info(message)
    if @level <= 2
      log("Info", message)
    end
  end

  def error(source, message)
    if @level < 3
      log("Error", message)
    end

    @errors.puts "[Source]: #{source}"
    @errors.puts "[Error]: #{message}"

    puts "[Source]: #{source}"
    puts "[Error]: #{message}"
  end

  def error_message(message)
    @errors.puts "#{message}"
  end

  def close
    if @log
      @log.close
    end
    @errors.close
  end
end
