require "#{File.dirname(__FILE__)}/../test_helper"

class AssociatedFilesTest < ActionController::IntegrationTest
  fixtures :articles, :associated_files

  def setup
    @associated_files = [:art4files, :art5files]
    @pdf = Tempfile.new("pdf")
    @pdf.write(File.open("#{RAILS_ROOT}/test/files/v17n01a01.pdf", 'r').read())
    @pdf.open()
    @html = Tempfile.new("html")
    @html.write(File.open("#{RAILS_ROOT}/test/files/v17n01a01.htm", 'r').read())
    @html.open()
    @myassociated_file = {:article_id => 9999, :filename => 'v17n01a01', :pdfdata => @pdf, :htmldata => @html}
  end

   def test_getting_index
     get "/associated_files"
     assert_equal 200, status
     assert_equal '/associated_files', path
   end

   def test_new
     get "/associated_files/new"
     assert_equal 200, status
     assert_equal '/associated_files/new', path
   end

#    def  test_creating_new_associated_files
#      post "associated_files/create", :record => @myassociated_file
#      assert_equal 302, status
#      follow_redirect!
#      assert_equal '/associated_files/list', path
#    end

   def test_showing
     @associated_files.each { | associated_file |
       post "/associated_files/show", :id => associated_files(associated_file).id
       assert 200, status
       assert_equal '/associated_files/show', path
     }
   end

   def test_editing
     @associated_files.each { | associated_file |
       post "/associated_files/edit", :id => associated_files(associated_file).id
       assert 200, status
       assert "/associated_files/edit/#{associated_files(associated_file).id}", path
     }
   end

   def test_updating
     @associated_files.each { | associated_file |
       post "/associated_files/update", :id => associated_files(associated_file).id, :filename => associated_files(associated_file).filename.reverse
       assert 302, status
       follow_redirect!
       assert "/associated_files/show/#{associated_files(associated_file).id}", path
     }
   end

    def test_deleting
     @associated_files.each { | associated_file |
       get "/associated_files/destroy", :id => associated_files(associated_file).id
       assert 302, status
       assert '/associated_files/list',  path
     }
   end
end
