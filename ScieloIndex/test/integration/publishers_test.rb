require "#{File.dirname(__FILE__)}/../test_helper"

class PublishersTest < ActionController::IntegrationTest
  fixtures :publishers

  def setup
    @publishers = [:mit, :white, :black]
  end

   def test_getting_index
     get "/publishers"
     assert_equal 200, status
     assert_equal '/publishers', path
   end

   def test_showing
     @publishers.each { | publisher |
       post "/publishers/show", :id => publishers(publisher).id
       assert 200, status
       assert_equal '/publishers/show', path
     }
   end

   def test_editing
     @publishers.each { | publisher |
       post "/publishers/edit", :id => publishers(publisher).id
       assert 200, status
       assert "/publishers/edit/#{publishers(publisher).id}", path
     }
   end

   def test_updating_name
     @publishers.each { | publisher |
       post "/publishers/update", :id => publishers(publisher).id, :name => publishers(publisher).name.reverse, :descr => publishers(publisher).descr.reverse, :url => publishers(publisher).url.reverse
       assert 302, status
       follow_redirect!
       assert "/publishers/show/#{publishers(publisher).id}", path
     }
   end

    def test_deleting
     @publishers.each { | publisher |
       get "/publishers/destroy", :id => publishers(publisher).id
       assert 302, status
       assert '/publishers/list',  path
     }
   end
end
