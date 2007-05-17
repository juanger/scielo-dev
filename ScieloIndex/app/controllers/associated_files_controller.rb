class AssociatedFilesController < ScieloIndexController
  def initialize
    @model = AssociatedFile
    @created_msg = "The files were created!"
    @updated_msg = "The files were updated!"
  end

  def create
    @record = @model.new
    @record.filename = params[:record][:filename]
    @record.pdfdata = params[:record][:pdfdata].read
    @record.htmldata = params[:record][:htmldata].read

    if @record.save
      flash[:notice] = @created_msg
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end

  def send_file
    @record = @model.find(params[:id])

    if params[:format] == 'PDF'
      send_data @record.pdfdata, :filename => @record.name + ".pdf", :type => 'application/pdf'
    elsif params[:format] == 'HTML'
      send_data @record.htmldata, :filename => @record.name + ".htm", :type => 'text/html'
    end
  end
end
