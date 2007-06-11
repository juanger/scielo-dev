#!/usr/bin/env ruby
RAILS_ENV = 'development'
require File.dirname(__FILE__) + '/../../config/environment'

class Migrator
  def initialize
    config = "./config"
    if File.file? config
      open(config, "r") { |file|
        file.each { |line|
          next if line =~ /^#.*$/
          obtain_value line}
      }
    else
      puts "Por favor crear un archivo config."
    end
  end

  def obtain_value(line)
    array = line.split(':')
    case array[0]
      when 'SERIAL_ROOT'
                @serial_root = array[1].strip
                #puts 'SERIAL_ROOT ' + array[1]
      when 'COUNTRY'
                @origin_country = array[1].strip
                #puts 'COUNTRY ' + array[1]
      else
                puts 'Archivo de configuración ilegal'
    end
  end

  def process_scielo
    if File.directory? @serial_root
      Dir.foreach(@serial_root) { |dir|
                next if dir =~ /^(\.|code|issue|issn|section|title|titleanterior)\.?$/

                full_dir = File.join(@serial_root, dir)
                if File.directory? full_dir
                  @current_journal = dir
                  process_journal(full_dir)
                end
      }
    end
  end

  def process_journal(journal_dir)
    puts "Migrando la revista: " + @current_journal

    if File.directory? journal_dir
      Dir.foreach(journal_dir) { |issue_dir|
                next if issue_dir =~ /^(\.|_notes|paginasinformativas)\.?$/

                full_dir = File.join(journal_dir, issue_dir)
                if File.directory? full_dir
                  @current_issue = issue_dir
                  process_issue(full_dir)
                end
      }
    end
  end

  def process_issue(issue_dir)
    puts "Migrando numero: " + @current_issue
    pdf_dir = File.join(issue_dir, "pdf")

    if File.directory? pdf_dir
      Dir.foreach(pdf_dir) { |pdf|
        full_dir = File.join(pdf_dir, pdf)
        next unless pdf =~ /^.*(\.)(pdf|PDF)$/

        if File.file? full_dir
          @current_article = pdf
          process_pdf(full_dir)
        end
      }
    else
      puts "El numero no contiene documentos PDF"
    end
  end

  def process_pdf(pdf_dir)
    puts "Procesando articulo: " + @current_article
  end
end

# Country.find(:all).each { |country|
#   puts "Id: #{country.id}, Nombre: #{country.name}, Code: #{country.code}"
# }

# Publisher.find(:all).each { |publisher|
#   puts "id: #{publisher.id}, Titulo: #{publisher.name}"
# }

# Journal.find(:all).each { |journal|
#   puts "id: #{journal.id}, Title: #{journal.title}, Country: #{journal.country.name}, Publisher: #{journal.publisher.name}, Abbrev: #{journal.abbrev}, ISSN: #{journal.issn}"
# }

migrator = Migrator.new()
migrator.process_scielo()
