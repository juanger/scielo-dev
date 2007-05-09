class CountriesController < ScieloIndexController

  def initialize
    @model = Country
    @created_msg = 'The country was created'
    @updated_msg = 'The country was updated'
  end

  def create
    @record = @model.new(params[:record])
    @record.id = params[:record][:id]

    if @record.save
      flash[:notice] = @created_msg
      redirect_to :action => 'list'
    else
      render :action => 'new'
    end
  end
end
