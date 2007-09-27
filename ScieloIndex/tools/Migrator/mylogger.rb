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

  def error(message)
    if @level < 3
      log("Error", message)
      puts "[Error]: #{message}"
    else @level == 3
      puts "[Error]: #{message}"
    end
  end

  def close
    if @log
      @log.close
    end
  end
end
