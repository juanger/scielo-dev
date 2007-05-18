class AssociatedFilesController < ScieloIndexController

  DIR = "#{RAILS_ROOT}/public/associated_files"

  def initialize
    @model = AssociatedFile
    @created_msg = "The files were uploaded!"
    @updated_msg = "The files were updated!"
  end

  def create
    @record = @model.new
    @record.article_id = params[:record][:article_id]
    @record.filename = params[:record][:filename]

    if @record.article_id and @record.filename.size > 2
      dir = "#{DIR}/#{@record.article_id}"

      if !File.exists?(dir)
        Dir.mkdir(dir)
      end

      pdf_stream = params[:record][:pdfdata]
      html_stream = params[:record][:htmldata]
      if pdf_stream.class == Tempfile and html_stream.class == Tempfile
        @record.pdf_path = dir + '/'+ @record.filename + '.pdf'
        File.open(@record.pdf_path, "wb") do |f|
          f.write(pdf_stream)
        end

        @record.html_path = dir + '/' + @record.filename + '.htm'
        File.open(@record.html_path, "wb") do |f|
          f.write(html_stream)
        end
      end
    end

    if @record.save
      flash[:notice] = @created_msg
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def destroy
    @record = @model.find(params[:id])
    delete_directory("#{DIR}/#{@record.article_id}")
    @model.find(params[:id]).destroy
    redirect_to :action => 'list'
  end

  def send_file
    @record = @model.find(params[:id])

    if params[:format] == 'PDF'
      @pdf_file = File.open(@record.pdf_path)
      send_data @pdf_file.read, :filename => @record.filename + ".pdf", :type => 'application/pdf'
    elsif params[:format] == 'HTML'
      @html_file = File.open(@record.html_path)
      send_data @html_file.read, :filename => @record.filename + ".htm", :type => 'text/html'
    end
  end

  private

  def delete_directory(dir)
    Dir.foreach(dir) {|file|
      next if file =~ /^\.\.?$/
      file = dir + '/' + file
      File.delete(file)
    }
    Dir.delete(dir)
  end
end
