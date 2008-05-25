class MyLogger

  def initialize(level)
   @levels = {
    :debug => 0,
    :warning => 1,
    :info => 2,
    :none => 3
   }
    @level = @levels[level]
    if @level < 3
      @log = File.new("migrator-log", "w")
    end
    @errors = File.new("migrator-errors", "w")
  end

  def log(type, message, indent = "")
    if @log
      @log.puts "#{indent}[#{type}]: #{message}"
    end
  end
  
  def error(source, message)
    if @level < 3
      log("Source", source)
      log("Error", message)
    end

    @errors.puts "[Source]: #{source}"
    @errors.puts "[Message]: #{message}"

    puts "[Source]: #{source}"
    puts "[Message]: #{message}"
  end

  def error_message(message)
    if @level < 3
      log("Error", message)
    end

    puts "[Error]: #{message}"
    @errors.puts "[Error]: #{message}"
  end

  ## Metaprogramed methods for levels
  [:debug, :warning, :info].each do |level|
    code = "def #{level}(message, indent = \"\")
              if @level <= @levels[:#{level}]
                log(:#{level},
                    message,
                    indent)
              end
            end"  
    class_eval(code)
  end

  def close
    if @log
      @log.close
    end
    @errors.close
  end
end