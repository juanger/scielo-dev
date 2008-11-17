class MyLogger

  def initialize(level, log_file="migrator-log", errors_file="migrator-errors",verbose=true)
   @levels = {
    :debug => 0,
    :warning => 1,
    :info => 2,
    :none => 3
   }
    @level = @levels[level]
    if @level < 3
      @log = File.new(log_file, "w")
    end
    @verbose = verbose
    @errors = File.new(errors_file, "w")
    @pdf = ScieloIndexReport.new()
    @pdf.ErrorReportTitle
    @pdf.SetFont("vera", "", 10);
  end

  def log(type, message, indent = "")
    @log.puts "#{indent}[#{type}]: #{message}" if @log
  end
  
  def error(source, message)
    if @level < 3
      log("Source", source)
      log("Error", message)
    end

    @errors.puts "[Source]: #{source}"
    @errors.puts "[Message]: #{message}"

    if @verbose
      puts "[Source]: #{source}"
      puts "[Message]: #{message}"
    end
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

  def pdf_report_journal(name)
    @pdf.writeHTML("<h1>#{name}</h1>")
  end
  
  def pdf_report_error(source, error_msg)
    @pdf.writeHTML("<p><strong>#{source}</strong>  #{error_msg}</p>")
  end


  def close
    @log.close if @log
    @pdf.Output("error_report.pdf", 'F')
    @errors.close
  end
end