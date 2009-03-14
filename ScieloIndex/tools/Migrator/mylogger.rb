require "#{RAILS_ROOT}/lib/pdf_writer_extensions"

class MyLogger

  INDENT = "\t"

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
    @pdf = PDF::Writer.new()
  end

  def log(type, message, indent = "")
    @log.puts "#{indent}[#{type}]: #{message}" if @log
    puts "[#{type}]: #{message}" if @verbose
  end
  
  def error(source, message)
    if @level < 3
      log("Source", source)
      log("Error", message)
    end

    @errors.puts "\t[Source]: #{source}"
    @errors.puts "\t[Message]: #{message}"
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
    @pdf.text(name, :font_size => 16)
  end
  
  def pdf_report_error(source, error_msg)
    @pdf.text(source+ "\n", :left => 20, :font_size => 12)
    @pdf.text(error_msg, :left => 50, :font_size => 12)
  end


  def close
    @log.close if @log
    @pdf.save_as("error_report.pdf")
    @errors.close
  end
end